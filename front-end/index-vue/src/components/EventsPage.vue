<template>
    <div class = "row">
        <div class = "search-box">
            <h1>Events</h1>
        </div>
        <div class = "flex-box">
            <div>
                <div v-for="event in eventDataList" :key="event.title" class="events-container">
                    <article class="post">
                        <img class="event-img" v-bind:src='event.image'>
                        <div class="description-box">
                            <RouterLink :to="{ path: member.bioLink}">
                                <h2 class="post-excerpt-title">{{event.title}}</h2>
                            </RouterLink>
                            <div><i class="fa fa-calendar-day"></i><b>{{event.start_date}}</b></div>
                            <div><i class="fa fa-clock"></i><b>{{event.time_range}}</b></div>
                            <p>{{event.description}}</p>
                        </div>
                    </article>
                </div>
            </div>
            <div class = "subscribe-events">
                <h3>Never miss an event!</h3>
                <p>Get event announcements and other DAC news sent right to your inbox!</p>
                <input placeholder="Enter your email address">
            </div>
        </div>
    </div>
</template>

<script>
    //import events from json file
    import axios from "axios";
    export default {
        name: "eventList",
        data() {
            return {
                eventDataList: []
            };
        },
        methods: {
            getEventData() {
                axios
                    .get("events.json")
                    .then(response => (this.eventDataList = response.data));
            }
        },
        beforeMount()
        {
        this.getEventData();
        }
    };
    
</script>

<style>
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

    .post-excerpt-title {
        margin: 0;
        padding: 0;
        font-size: 1.7em;
        text-transform: capitalize;
        color: #3e79a8;
        text-decoration: none;
    }

    a{
        text-decoration: none;
    }

    a:hover, :focus {
		color: inherit;
		text-decoration: underline;
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