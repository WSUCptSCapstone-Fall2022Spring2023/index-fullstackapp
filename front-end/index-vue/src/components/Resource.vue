<template>
    <div class = "row">
        <div class = "page-header-wrapper">
            <span class = "page-title" >Resource</span>
        </div>
        <div class = "flex-box">
            <div class="entry-content">
                <h1 class="post-title">{{ resourceJSON.pageTitle}}</h1>
                <div class="resources">
                    <li class = "bulletpoints" v-for="bulletpoints in resourceJSON.bulletpoints" :key="bulletpoints.title">
                        <a v-bind:href='bulletpoints.link'>
                            <h3>{{ bulletpoints.title }}</h3>
                        </a>
                        <p>{{ bulletpoints.description}}</p>
                    </li>
                </div>
            </div>
            <div class = "sidebar">
                <h4 class="widget-title">Related Pages</h4>
                <ul class="related-pages">
                    <li v-for="resource in resourceList" :key="resource.pageTitle">
                    <a v-bind:href='resource.pageLink'>
                        <i v-bind:class='resource.pageIcon'></i>
                        {{resource.pageTitle}}
                    </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>
    //import events from json file
    import axios from "axios";
    export default {
        name: "resourceList",
        data() {
            return {
                resourceList: [],
                resourceJSON: {}
            };
        },
        methods: {
            getResourceData() {
                axios
                    .get("../../Resources.json")
                    .then((response) => {
						this.resourceList = response.data.Resources;
                        for(var resource of this.resourceList)
                        {
                            if(resource.pageLink)
                            {
                                if(resource.pageLink.includes(this.$route.params.id))
                                {
                                    this.resourceJSON = resource;
                                }
                            }
                        }
                    }
                    );
            }
        },
        beforeMount()
        {
            this.getResourceData();
        }
    };
    
</script>

<style scoped>
    .row {
        margin: 0 auto;
        max-width: 1200px;
    }

    .flex-box{
        display: grid;
        grid-auto-flow: column;
        grid-column-gap: 1px;
    }

    .page-title {
        display: block;
        max-width: 85%;
        font-size: 1.8em;
        line-height: 1.2em;
        font-weight: 400;
        font-size: 3.5em;
        text-align: left;
    }

    .entry-content{
        text-align: left;
        display: block;
        float: left;
    }

    .entry-content a {
        color: #376c95;
        text-decoration: underline;
    }

    .entry-content h3 {
        margin: 0;
        padding: 0;
    }

    .entry-content p {
        line-height: 1.65em;
        margin: 0;
        padding: 0;
    }

    .post-title {
        margin-bottom: 0;
        font-weight: 700;
    }

    .page-header-wrapper{
        padding: 51px 0;
    }

    .sidebar {
        display: inline;
        float: left;
        width: 275px;
        padding: 60px 0px;
        margin-left: 100px;
        text-align: left;
        padding: 0;
    }

    .widget-title {
        padding-bottom: 7px;
        border-bottom: 1px solid #ddd;
        color: #df6c18;
    }

    .related-pages {
        list-style: none;
        margin: 0;
        padding: 0;
    }

    .related-pages li{
        width: 100%;
        text-align: left;
    }

    .related-pages li a {
        display: block;
        margin: 3px 0px;
        padding: 7px 15px;
        border-radius: 3px;
        font-size: .9em;
    }

    .related-pages li a:hover, .related-pages li a:focus {
        background: #E87928;
        color: #fff;
    }

    .related-pages .fa {
        margin-right: 8px;
    }

    .related-pages i {
        width: 18px;
        text-align: center;
    }

    a {
        color: inherit;
        text-decoration: inherit;
    }

    a:hover, a:focus {
        color: inherit;
        text-decoration: underline;
    }

    ul {
        line-height: 1em;
    }

    .resources {
        margin: 0;
        padding: 0;
        padding-top: 20px;
        list-style: none;
    }

    .bulletpoints{
        display: block;
        text-align: left;
        padding-bottom: 30px;
    }

    @media screen and (max-width: 715px) {
        .flex-box{
            display: block;
        }
        .sidebar {
            margin: 0;
            width: 100%;
        }
    }
</style>