<template>
    <div class = "row">
        <div class = "search-box">
            <h1>Newsletter</h1>
        </div>
        <div class = "flex-box">
            <div>
                <div v-for="news in newsDataList" :key="news.title" class="events-container">
                    <article class="post">
                        <img class="event-img" v-bind:src='news.image'>
                        <div class="description-box">
                            <RouterLink :to="{ path: news.newsLink}">
                                {{news.title}}
                            </RouterLink>
                            <div class = "time">{{news.date}}</div>
                            <p>{{news.description}}</p>
                        </div>
                    </article>
                </div>
            </div>
            
        </div>
    </div>
</template>

<script>
    //import events from json file
    import axios from "axios";
    export default {
        name: "newsList",
        data() {
            return {
                newsDataList: []
            };
        },
        methods: {
            getNewsData() {
                axios
                    .get("news.json")
                    .then(response => (this.newsDataList = response.data.newsItems));
            }
        },
        beforeMount()
        {
        this.getNewsData();
        }
    };
    
</script>

<style scoped>
    a {
         color: #3e79a8;
         text-decoration: none;
         font-weight: bold;
         font-size: 1.7em;
    }

    .time {
        color: #295170;
        font-weight: bold;
    }

    .row {
        margin: 0 auto;
        max-width: 1200px;
    }

    .flex-box{
        display: grid;
        grid-auto-flow: column;
        grid-column-gap: 1px;
    }

    .search-box{
        text-align: left;
    }

    .subscribe-events{
        border: 1px solid gray;
        width: 220px;
        height: 200px;
        padding: 10px;
    }

    .subscribe-events h3{
        color: #E87928;
    }

    .events-container{
        max-width: 800px;
    }

    .post{
        display: flex;
        border: 1px solid gray;
        padding: 20px;
        grid-column-gap: 20px;
        margin-bottom: 20px;
    }
    .event-img{
        width: auto;
        max-height: 225px;
        max-width: 338px;
        height: auto;
    }

    .description-box{
    text-align: left;
    }

    @media screen and (max-width: 715px) {
        .post{
            display: block;
        }

        .search-box{
            text-align: center;
        }

        .flex-box{
            display: block;
        }

        .subscribe-events{
           width: auto;
           height: auto;
        }
    }
    
</style>