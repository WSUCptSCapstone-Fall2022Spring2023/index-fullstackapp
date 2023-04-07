<template>
    <div class = "row">
        <div class = "page-header-wrapper">
            <span class = "page-title" >Programs & Services</span>
            <p class="page-description">People with disabilities don’t have special needs, they have the same needs as everyone else.</p>
        </div>
        <div class = "flex-box">
            <div>
                <article class="entry-content">
                    <h1 class="post-title">{{specialtyJSON.name}}</h1>
                    <p class="post-tagline">{{specialtyJSON.subtitle}}</p>
                    <p class = "content">
                        <a v-bind:href='specialtyJSON.image'>
                            <img v-bind:src='specialtyJSON.image'>
                        </a>
                    </p>
                    <p>{{specialtyJSON.description}}</p>
                </article>
                <p class = "bulletpoints" v-for="bulletpoints in specialtyJSON.bulletpoints" :key="bulletpoints">
                    {{bulletpoints }}
                </p>
            </div>
            <div class = "sidebar">
                <h4 class="widget-title">Related Pages</h4>
                <ul class="related-pages">
                    <li v-for="specialty in specialtiesJson" :key="specialty.name">
                    <a v-bind:href='specialty.link'>
                        <i v-bind:class='specialty.icon'></i>
                        {{specialty.name}}
                    </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    
</template>

<script>
    //import members from json file
    import axios from "axios";
    export default {
        name: "Specialty",
        data() {
            return {
                specialtiesJson: [],
                specialtyJSON: {}
            };
        },
        methods: {
            getSpecialtyData() {
                axios
                    .get("../../specialties.json")
                    .then((response) => {
						this.specialtiesJson = response.data.specialties;
                        for(var specialty of this.specialtiesJson)
                        {
                            if(specialty.link)
                            {
                                if(specialty.link.includes(this.$route.params.id))
                                {
                                    this.specialtyJSON = specialty;
                                }
                            }
                        }
                    }
                );
            }
        },
        beforeMount()
        {
            this.getSpecialtyData();
        }
    };
    
</script>

<style scoped>
    .page-title {
        display: block;
        max-width: 85%;
        font-size: 1.8em;
        line-height: 1.2em;
        font-weight: 400;
        font-size: 3.5em;
        text-align: left;
    }

    .page-header-wrapper{
        line-height: 0;
        padding: 51px 0;
    }

    .page-description{
        font-style: normal;
        font-weight: 400;
        font-size: 1.3em;
        color: #a2c3dd;
        text-transform: capitalize;
        word-spacing: 0.1em;
        text-align: left;
    }

    .flex-box{
        display: grid;
        grid-auto-flow: column;
        grid-column-gap: 1px;
    }

    .entry-content{
        text-align: left;
        display: block;
        float: left;
    }

    .entry-content p, .entry-content ul {
        font-size: 1em;
        margin-bottom: 30px;
        line-height: 1.625em;
    }

    .post-title {
        margin-bottom: 0;
        font-weight: 700;
    }

    .content{
        float: left;
    }

    .bulletpoints{
        text-align: left;
        line-height: 1.625em;
    }

    p.post-tagline {
        margin-top: 2px;
        color: #305e83;
        font-weight: 400;
        font-size: 1.3em;
        line-height: 1.3em;
    }

    img{
        height: auto;
        width: 350px;
        margin: 10px 30px 30px 0px;
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
</style>