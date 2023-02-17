<template>
    <div class = "row">
        <div class="page-header-wrapper">
			<span class="page-title">{{memberJson.name}}</span>
		</div>
        <div class = "flex-box">
            <main class="main-section" id="main-content" role="main">
                <article class="entry-content">
                    <img v-bind:src='memberJson.image' class="profile-picture wp-post-image">
                    <div>
                        <p>{{memberJson.bioDescription}}</p>
                        <p>{{memberJson.bioQuote}}</p>
                    </div>
                </article>
            </main>
            <aside class = "sidebar">
                <h4 class="widget-title">About {{memberJson.name}}</h4>
                <dl class="contact-info">
			
                    <dt>Position:</dt>
                    <dd>{{ memberJson.position }}</dd>
                    <dt>Employee Since:</dt>
                    <dd>{{memberJson.employeeSince}}</dd>
                    <dt>Specialties:</dt>
                    <div v-for="specialty in memberJson.specialties" :key="specialty.name">
                        <dd>
                            <a v-bind:href='specialty.link'><i class="fa fa-users"></i>{{specialty.name}}</a>
                        </dd>
                    </div>                
                </dl>
            </aside>
        </div>
    </div>

</template>

<script>
//import members from json file
import axios from "axios";
    export default {
        name: "AboutUs",
        data() {
            return {
                memberData: {},
                memberJson: {}
            };
        },
        methods: {
            getMemberData() {
                axios
                    .get("../../members.json")
                    .then((response) => {
						this.memberData = response.data;
                        for(var member of response.data.boardMembers)
                        {
                            if(member.bioLink.includes(this.$route.params.id))
                            {
                                this.memberJson = member;
                            }
                        }
                    }
                );
            }
        },
        beforeMount()
        {
            this.getMemberData();
        }
    };
                                
                          
</script>

<style scoped>
.row {
    margin: 0 auto;
    padding: 0px 10px;
    max-width: 1200px 
}

.flex-box{
        display: grid;
        grid-auto-flow: column;
        grid-column-gap: 1px;
}

.page-header-wrapper {
    padding: 31px 0px;
    text-align: left;
}

.page-title {
    display: block;
    max-width: 100%;
    font-size: 1.8em;
    line-height: 1.2em;
    font-weight: bold;
}

.main-section {
    display: inline;
    float: left;
    width: 98.33333%;
    margin: 0 0.83333%;
    padding: 30px 0px;
}

article {
    display: block;
    text-align: justify;
} 

.profile-picture {
    float: right;
    margin-bottom: 5%;
    margin-left: 10%;
    max-width: 40%;
    border: 1px solid #e8e9eb;
    border-radius: 50%;
    overflow-clip-margin: content-box;
    overflow: clip;
}


.sidebar {
    display: inline;
    float: left;
    margin: 0 0.83333%;
    margin-left: 30%;
    padding: 60px 0px;
}

.widget-title {
    padding-bottom: 7px;
    border-bottom: 1px solid #ddd;
    color: #df6c18;
}

dt {
    display: block;
}

dd {
    margin: 0;
    padding: 0;
    border: 0;
    vertical-align: baseline;
    font: inherit;
    font-size: 100%;
}
.contact-info{
    text-align: left;
}

.contact-info dd {
    padding-bottom: 12px;
}

.contact-info a {
    display: block;
    margin: 3px 0px;
    padding: 7px 15px;
    border-radius: 3px;
}

.contact-info .fa {
    margin-right: 8px;
}

.entry-content{
    max-width: 700px;
}

a {
	color: inherit;
	text-decoration: inherit;
}

a:hover, :focus {
		color: inherit;
		text-decoration: underline;
}
</style>