<template>
    <div class = "row">
        <div>
            <h1 class = "title" >Board Members</h1>
        </div>
        <div class = "content">
            <p v-html="pageDescription"></p>
            <ul class="location-staff">
				<div v-for="member in memberJson.boardMembers" :key="member.name">
					<li>
						<RouterLink :to="{ path: member.bioLink}">
							<img width="200" height="200" v-bind:src='member.image' class="round-thumb no-alt wp-post-image" decoding="async" sizes="(max-width: 200px) 100vw, 200px">								
							<span class="staff-name">{{member.name}}</span>
						</RouterLink>
						<span class="staff-position">{{member.position}}</span>
					</li>
				</div>
			</ul>
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
                memberJson: {},
				pageDescription: ""
            };
        },
        methods: {
            getMemberData() {
                axios
                    .get("members.json")
                    .then(response => (
						this.memberJson = response.data,
						this.pageDescription = response.data.pageDescription.replace(response.data.phraseLink,"<a href=\"" + response.data.applicationLink + "\">" + response.data.phraseLink + "</a>")
					));
            }
        },
        beforeMount()
        {
        this.getMemberData();
        }
    };
    
</script>

<style scoped>
    .title {
        text-align: left;
    }

    .content{
        margin: 0 auto;
        max-width: 1000px;
        text-align: left;
        color: grey;
    }

    .content p{
        font-weight: bold;
    }

    .content a{
        color: orange;
    }

    ul.location-staff {
        clear: both;
        width: 100%;
        display: block;
        list-style: none;
        margin: 0;
        padding: 0;
    }

    ul.location-staff li {
        float: left;
        width: 44%;
        margin: 3%;
        text-align: center;
    }

    .round-thumb {
        border: 1px solid #e8e9eb;
        border-radius: 50%;
    }

    .staff-name {
        display: block;
        margin: 0;
        padding: 12px 0 0;
        font-weight: 700;
        font-size: 1.125em;
    }

    @media only screen and (min-width: 40.063em){
        ul.location-staff li {
            width: 27.33333%;
        }
    }
</style>