<template>
    <p>{{eventJson.title}}</p>
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
                        for(var event of response.data)
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

</style>