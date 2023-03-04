<template>
    <div class = "row">
        <div class="flex-box">
            <div class ="align-left">
                <span class = "byline">{{ newsJson.postedBy }}</span>
                <h1 class = "title">{{ newsJson.title }}</h1>
                <img v-bind:src='newsJson.image' class="news-image">
                <p>{{newsJson.description}}</p>
            </div>
            <div class = "subscribe-events">
                <h3>Subscribe via email</h3>
                <p>Get the monthly newsletter sent directly to your inbox!</p>
                <input placeholder="Enter your email address">
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
    export default {
        name: "Event",
        data() {
            return {
                newsJson: {}
            };
        },
        methods: {
            getMemberData() {
                axios
                    .get("../../news.json")
                    .then((response) => {
                        for(var event of response.data.newsItems)
                        {
                            if(event.newsLink.includes(this.$route.params.id))
                            {
                                this.newsJson = event;
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
        padding: 10px 10px;
        max-width: 1200px 
    }

    .title{
        margin: 0;
    }

    time, .byline {
        color: #295170;
        font-weight: bold;
    }

    .align-left{
        text-align: left;
    }

    .flex-box{
        display: flex;
        gap: 15px;
    }

    .news-image{
        width: 100%;
    }

    .subscribe-events{
        border: 1px solid gray;
        width: 220px;
        height: 200px;
        padding: 10px;
    }

</style>