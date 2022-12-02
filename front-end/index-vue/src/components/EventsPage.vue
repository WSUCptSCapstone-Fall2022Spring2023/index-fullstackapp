<template>
    <div class = "row">
        <div v-for="event in events" :key="event.title" class="events-container">
            <article class="post">
                <img class="event-img" v-bind:src='event.image'>
                <div class="description-box">
                    <h2>{{event.title}}</h2>
                    <div><i class="fa fa-calendar-day"></i><b>{{event.start_date}}</b></div>
                    <div><i class="fa fa-clock"></i><b>{{event.time_range}}</b></div>
                    <p>{{event.description}}</p>
                </div>
            </article>
        </div>
    </div>
</template>

<script>
    let json = require('../../public/events.json');
    export default {
        data(){
            return {
                events : json
            }
        }
    }
    
</script>

<style>
    .row {
        margin: 0 auto;
        max-width: 1200px;
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

    
</style>