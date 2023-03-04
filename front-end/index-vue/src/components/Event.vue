<template>
    <div class = "row">
        <h1 class = "title">{{ eventJson.title }}</h1>
        <div class = "flex-box event-box">
            <img v-bind:src='eventJson.image' class="profile-picture wp-post-image">
            <div>
                <div class = "flex-box info-box">
                    <div><i class="icon fa fa-calendar"></i>{{ eventJson.start_date }}</div>
                    <div><i class="icon fa fa-clock"></i>{{ eventJson.time_range }}</div>
                </div>
                <div class = "info-box">
                    <div>
                        <span class="event-location-details"><i class="icon fa fa-map-marker-alt"></i>Rehabilitation Hospital of the NW</span>
                    </div>
                </div>
                <div class = "flex-box info-box">
                    <a class="anchor-button" href="http://maps.google.com/maps?daddr=3372 E. Jenalan, Post Falls, ID">Get Directions</a>
                    <a class="anchor-button" href="https://www.facebook.com/groups/1733229843583267/">RSVP on Facebook</a>
                </div>
            </div>
        </div>
        <p>{{ eventJson.description }}</p>
    </div>
</template>

<script>
import axios from "axios";
    export default {
        name: "Event",
        data() {
            return {
                eventJson: {}
            };
        },
        methods: {
            getMemberData() {
                axios
                    .get("../../events.json")
                    .then((response) => {
                        for(var event of response.data.Events)
                        {
                            if(event.link.includes(this.$route.params.id))
                            {
                                this.eventJson = event;
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

    .row p{
        text-align: left;
    }

    .title{
        text-align: left;
    }

    .flex-box{
        display: flex;
        gap: 15px;
    }

    .event-box{
        width: 70%;
    }

    .icon{
        margin-right: 8px;
    }

    .event-location-details{
        text-align: left;
    }

    .info-box{
        float: left;
        margin-bottom: 10px;
    }

    .anchor-button{
        background: linear-gradient(#376c95, #295170 50%);
        border: 0;
        font-weight: 400;
        font-size: 1em;
        line-height: 1em;
        color: #fff;
        cursor: pointer;
        padding: 8px 12px 8px;
        margin: 0px 10px 10px 0px;
    }

</style>