<template>
    <!-- Navbar Section -->
    <nav id = "navbar" class="navbar">
      <div class="navbar__container">
        <img src="../assets/logo.png" style ="width: 60px;height: 60px;">
        <div class="navbar__toggle" id="mobile-menu">
          <span class="bar"></span> 
          <span class="bar"></span>
          <span class="bar"></span>
        </div>
        <ul class="navbar__menu">
          <li class="navbar__item">
            <a class="navbar__links">Home</a>
          </li>
          <li class="navbar__item">
            <a class="navbar__links">About us</a>
          </li>
          <li class="navbar__item">
            <a class="navbar__links">Get Involved</a>
          </li>
          <li class="navbar__item">
            <a class="navbar__links">Contact</a>
          </li>
          <li class="navbar__item">
            <a class="navbar__links">Resources</a>
          </li>
          <li class="navbar__item">
            <a class="navbar__links">Services</a>
          </li>
          <li class="navbar__item">
            <a class="navbar__links">Events</a>
          </li>
        </ul>
      </div>
    </nav>

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
  window.onload = function(){

    // Display Mobile Menu
    const menu = document.querySelector('#mobile-menu');
    const menuLinks = document.querySelector('.navbar__menu');
    const mobileMenu = () => {
      menu.classList.toggle('is-active');
      menuLinks.classList.toggle('active');
    };
    menu.addEventListener('click', mobileMenu);
  };
  //reading events.json file
  import axios from "axios";
    export default {
        name: "eventList",
        data() {
            return {
                eventDataList: []
            };
        },
        methods: {
            getEventData()
            {
               axios.get("events.json").then(response => (this.eventDataList = response.data));
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
ul {
  list-style-type: none;
  padding: 0;
}
/* Nav Bar Section */

.navbar {
  height: 60px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 16px;
  position: sticky;
  top: 0;
  z-index: 999;
  transition: 0.2s linear all;
  padding: 10px;
}

.navbar__links {
  color: black;
  display: flex;
  align-items: center;
  justify-content: center;
  text-decoration: none;
  height: 100%;
  transition: all 0.3s ease;
  cursor: pointer;
}

.navbar__container {
  display: flex;
  justify-content: space-between;
  z-index: 1;
  width: 100%;
  margin: 0 auto;
}

.navbar__menu {
  display: flex;
  align-items: center;
  list-style: none;
}

.navbar__item {
  padding: 10px;
}

.navbar__toggle{
  cursor: pointer;
}

.navbar__toggle .bar {
    display: block;
}

@media screen and (max-width: 715px) {
  .navbar__container {
    display: flex;
    justify-content: space-between;
    height: 80px;
    z-index: 1;
    width: 100%;
    max-width: 100%;
    padding: 0;
  }

  .navbar__menu {
    display: grid;
    grid-template-columns: auto;
    margin: 0;
    width: 100%;
    position: absolute;
    top: -1000px;
    opacity: 1;
    transition: all 0.5s ease;
    z-index: -1;
    right: 0;
  }

  .navbar__menu.active {
    top: 100%;
    opacity: 1;
    transition: all 0.5s ease;
    z-index: 99;
    font-size: 1.6rem;
  }

  .navbar__toggle .bar {
    width: 25px;
    height: 3px;
    margin: 5px auto;
    transition: all 0.3s ease-in-out;
    background: rgb(0, 0, 0);
  }

  .navbar__item {
    position: relative;
    width: 100%;
    padding: 0;
  }

  .navbar__links {
    text-align: center;
    padding: 2rem;
    left: 0;
    right: 0;
  }

  #mobile-menu {
    position: absolute;
    top: 20%;
    right: 5%;
    transform: translate(5%, 20%);
  }

  .navbar__toggle .bar {
    display: block;
    cursor: pointer;
  }

  #mobile-menu.is-active .bar:nth-child(2) {
    opacity: 0;
  }

  #mobile-menu.is-active .bar:nth-child(1) {
    transform: translateY(8px) rotate(45deg);
  }

  #mobile-menu.is-active .bar:nth-child(3) {
    transform: translateY(-8px) rotate(-45deg);
  }
}
</style>

<!-- <button v-on:click="getEventData">Get events Data</button>
<div>{{eventDataList}}</div> -->