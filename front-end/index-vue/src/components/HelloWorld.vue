<template>

<div>
    <h1>display events</h1>

    <div v-for="Event in eventDataList" :key="Event.id">
      <div>
        <div>
          <span>{{Event.title}}  {{Event.description}} {{Event.link}} {{Event.time}} {{Event.image}}</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//"@/assets/events.json"
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>

<!-- <button v-on:click="getEventData">Get events Data</button>
<div>{{eventDataList}}</div> -->