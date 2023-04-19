<template>
    <div class = "row">
        <div class ="map">
            <iframe src="https://www.google.com/maps/embed/v1/place?q=place_id:ChIJL0-D37vDYVMR5xFZhpF4dGM&key=AIzaSyAkURIooZWoXnYIJAHRuha5ZgjIJnO2YLQ" width="100%" height="350" style="border:0;"></iframe>
        </div>
        <div class = "flex-box">
            <div class="entry-content">
                <h1 class="post-title">{{ contactJSON.name}}</h1>
                <img class = "alignright" v-bind:src='contactJSON.imageLink'>
                <div v-for="content in contactJSON.content" :key="content.title">
                    <h2>{{content.title }}</h2>
                    <p>{{ content.description }}</p>
                </div>
            </div>
            <div class = "sidebar">
                <h4 class="widget-title">Contact {{contactJSON.name}}</h4>
                <ul class="related-pages">
                    <dl class="contact-info">
                        <dt>Phone:</dt>
                        <div v-for="phone in contactJSON.phone" :key="phone">
                            <dd>{{phone}}</dd>
                        </div>
                        <br>
                        <dt>Fax:</dt>
                        <dd>{{ contactJSON.fax }}</dd>
                        <br>
                        <dt>Physical Address:</dt>
                        <dd>{{ contactJSON.address }}</dd>
                        <br>
                        <dt>Hours:</dt>
                        <div v-for="hour in contactJSON.hours" :key="hour">
                            <dd>{{hour}}</dd>
                        </div> 
                    </dl>
                </ul>
            </div>
        </div>
        <div class = "align-center">
            <h2 class="section-title">
                <span class="alt-bg">Contact {{contactJSON.name}}</span>
            </h2>
            <p>Use the form below to send a message to {{contactJSON.name}}. You should receive a response shortly.</p>
            <form action="">
                <span class = "field">
                    <label class="field_label" for="input_1_1">Your Name:<span class="gfield_required">*</span></label>
                    <input aria-required="true" type="text">
                </span>
                <span class = "field">
                    <label class="field_label" for="input_1_1">Email:<span class="gfield_required">*</span></label>
                    <input aria-required="true" type="text">
                </span>
                <span class = "field">
                    <label class="field_label" for="input_1_1">Your Name:<span class="gfield_required">*</span></label>
                    <textarea aria-required="true" type="text"></textarea>
                </span>
                <input class="form_btn" type="submit">
            </form>
        </div>
    </div>
</template>

<script>
    //import events from json file
    import axios from "axios";
    export default {
        name: "ContactList",
        data() {
            return {
                ContactList: [],
                contactJSON: {}
            };
        },
        methods: {
            getcontactData() {
                axios
                    .get("../../Contact.json")
                    .then((response) => {
						this.contactList = response.data.location;
                        for(var contact of this.contactList)
                        {
                            if(contact.link)
                            {
                                if(contact.link.includes(this.$route.params.id))
                                {
                                    this.contactJSON = contact;
                                }
                            }
                        }
                    }
                    );
            }
        },
        beforeMount()
        {
            this.getcontactData();
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

    .alignright{
        max-width: 350px;
        margin: 10px 0px 30px 30px;
        float: right;
        width: 100%;
        height: auto;
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
        font-size: 1em;
        margin-bottom: 30px;
        line-height: 1.625em;
    }

    .post-title {
        font-weight: 700;
        color: #df6c18;
        font-size: 3.5em;
        line-height: 1.2em;
        display: block;
        margin: 0;
        padding: 0;
    }

    .page-header-wrapper{
        padding: 51px 0;
    }

    .contact-info{
        margin: 0;
        padding: 0;
        text-align: left;
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
        margin: 0;
    }

    .related-pages {
        list-style: none;
        margin: 0;
        padding: 0;
    }
    
    .bulletpoints{
        display: block;
        text-align: left;
        padding-bottom: 30px;
    }

    .map{
        margin-bottom: 30px;
    }

    h2{
        margin-top: 0;
        font-size: 1.8em;
    }

    dd{
        margin-inline-start: 0;
    }

    .section-title {
        background: url(https://dacnw.org/wp-content/themes/dac-nw/images/e8e9eb.png) 0 50% repeat-x;
        margin: 0;
    }

    .section-title span {
        display: inline-block;
        padding: 0px 10px;
        background: #fff;
        color: #295170;
    }

    .alt-bg{
        font-size: 42px;
    }

    .field_label{
        font-weight: bold;
    }

    .field{
        display: block;
        text-align: left;
        margin-top: 16px;
    }

    .field input{
        margin-top: 8px;
        width: 100%;
    }

    .field textarea{
        height: 160px;
        width: 100%;
        line-height: 1.5;
        resize: none;
    }

    .gfield_required{
        color: #790000;
    }

    .form_btn{
        background: linear-gradient(#376c95, #295170 50%);
        border: 0;
        border-bottom: 1px solid #0e1b25;
        border-radius: 2px;
        color: #ffffff;
        font-size: 1em;
        width: auto;
        margin: 10px 0 0 0;
        padding: 10px 20px 10px;
    }

    .row form{
        max-width: 450px;
        width: 100%;
        max-width: 450px;
        margin: 0 auto;
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