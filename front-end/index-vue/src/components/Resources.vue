<template>
    <div class = "row">
        <div class = "title">
            <h1>Resources</h1>
        </div>
        <ul class="child-pages">
            <li v-for="resource in resourceList" :key="resource.pageTitle">
                <a v-bind:href='resource.pageLink'>
                    <i v-bind:class='resource.pageIcon'></i>
                    {{resource.pageTitle}}
                </a>
            </li>
        </ul>
    </div>
</template>

<script>
    //import events from json file
    import axios from "axios";
    export default {
        name: "resourceList",
        data() {
            return {
                resourceList: []
            };
        },
        methods: {
            getResourceData() {
                axios
                    .get("Resources.json")
                    .then(response => (this.resourceList = response.data.Resources));
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

    .title{
        color: #df6c18;
        text-align: left;
    }

    .flex-box{
        display: grid;
        grid-auto-flow: column;
        grid-column-gap: 1px;
    }
    
    .child-pages {
        margin: 0;
        padding: 0;
        list-style: none;
        text-align: center;
        display: flex;
        flex-direction: row;
        gap: 5px;
        flex-grow: 3;
        flex-wrap: wrap;
    }


    .child-pages li {
        margin: 1%;
        width: 30.33333%;
        float: left;
        height: 159px;
    }

    .child-pages li a {
        background: #295170;
        border-bottom: #1b364b 4px solid;
        border-radius: 4px;
        color: white;
        text-align: center;
        width: 100%;
        height: 100%;
        display: block;
        font-weight: 700;
    }

    .child-pages li a .fa {
        padding-top: 25px;
        margin-right: 0px;
        padding-bottom: 15px;
        width: 100%;
        font-size: 4em;
    }   

    a {
        text-decoration: none;
    }

    @media screen and (max-width: 800px) {
        .child-pages li {
            width: 47.33333%;
        }
    }

    @media screen and (max-width: 550px) {
        .child-pages li {
            width: 100%;
        }

        .child-pages {
            flex-direction: column;
        }
    }
</style>