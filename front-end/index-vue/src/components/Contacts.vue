<template>
    <div class ="row">
        <div class = "search-box">
            <h1>Contact</h1>
        </div>
        <ul class="child-pages">
            <li v-for="contact in contactList" :key="contact.name">
                <a v-bind:href='contact.link'>
                    <img v-bind:src='contact.imageLink'>
                    <br>
                </a>
                <p></p>
                <h2>{{ contact.name }}</h2>
            </li>
        </ul>
        <div class = "align-center">
            <h2 class="section-title">
                <span class="alt-bg">Contact Us</span>
            </h2>
            <p>Use the form below to send us a message. You should receive a response shortly.</p>
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
        name: "contactList",
        data() {
            return {
                contactList: []
            };
        },
        methods: {
            getResourceData() {
                axios
                    .get("Contact.json")
                    .then(response => (this.contactList = response.data.location));
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
        padding: 10px 10px;
        max-width: 1200px 
    }

    .row form{
        max-width: 450px;
        width: 100%;
        max-width: 450px;
        margin: 0 auto;
    }

    .child-pages {
        padding: 40px;
        margin: 0 auto;
        list-style: none;
        text-align: center;
        display: flex;
        flex-direction: row;
        gap: 20px;
        flex-grow: 4;
        flex-wrap: wrap;
        align-items: center;
        justify-content: center;
    }


    .child-pages li {
        line-height: 10px;
    }

    .child-pages img {
        width: 267px;
        height: 165px;
        max-width: 340px;
    }

    .align-center{
        text-align: center;
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
</style>