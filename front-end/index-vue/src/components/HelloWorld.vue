<template>
  <div class = "row">
    <!-- Navbar Section -->
    <nav id = "navbar" class="navbar">
      <div class="navbar__container">
        <div class ="logo-box">
          <img src="../assets/logo.png" style ="width: 60px;height: 60px;">
          <div class = "site-title-block">
            <h2>INDEx</h2>
            <p>Inland Northwest Disability Experience</p>
          </div>
      </div>
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

    <!-- Navbar Section -->
    <!-- Main Section -->
    <div>
      <img class = "banner" src="../assets/homepage/banner.png">
      <div>
        <div class = "location-box">
          <ul class="locations">
            <li>
              <a href="https://dacnw.org/contact/moscow-id/">
                <span class="city-state">Moscow</span>
                <br>
                <span class="street-address">505 N Main St.</span>
              </a>
            </li>
            <li>
              <a href="https://dacnw.org/contact/lewiston-id/">
                <span class="city-state">Lewiston</span>
                <br>
                <span class="street-address">330 5th Street</span>
              </a>
            </li>
            <li>
              <a href="https://dacnw.org/contact/post-falls-id/">
                <span class="city-state">Post Falls</span>
                <br>
                <span class="street-address">3726 E Mullan Ave</span>
              </a>
            </li>
            <li>
              <a href="https://dacnw.org/contact/index/">
                <span class="city-state">INDEx, Spokane</span>
                <br>
                <span class="street-address">25 West Main</span>
              </a>
            </li>
          </ul>
          <form role="search" method="get" class = "search-box" action="https://dacnw.org/">
            <div>
                <input type="text" value="" name="s" id="s" class="search-input" aria-labelledby="search-desc">
                <input type="submit" class="search-btn" id="search-submit" value="Search">
            </div>
          </form>
        </div>
      </div>
    </div>
    
    <div class = "p-box">
      <div>
        <h2><a href="https://dacnw.org/about/">A hand up - not a hand out!</a></h2>
        <p>Independent Living means that we demand the same choices and control in our every-day lives that our non-disabled brothers and sisters, neighbors and friends take for granted. We want to grow up in our families, go to the neighborhood school, use the same bus as our neighbors, work in jobs that are in line with our education and interests and start families of our own.
          <br>Since we are the best experts on our needs, we need to show the solutions we want, need to be responsible and in charge of our lives, think and speak for ourselves, just as everybody else.</p>
      </div>
      <div>
        <h2><a href="https://dacnw.org/about/">We've been there</a></h2>
        <p>DAC prides itself on having an extremely diverse and adaptable board of directors, staff and volunteers. These people, many of whom have a disability, form the partnership to work to promote the independence and equality of all individuals with disabilities in all aspects of society.
        If COVID-19 has created a barrier to your independent living - give us a call!
        <br>Our Mission: DAC NW is building community, achieving equality and creating independence in an accessible world.</p>
      </div>
    </div>
    <div style = "height: 400px;position: relative;">
      <iframe class = "vertical-center" width="560" height="315" src="https://www.youtube.com/embed/_kjdEofI8sQ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
    </div>
    <!-- Main Section -->
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

    //Nav bar scroll listener
    window.addEventListener('scroll', highlightMenu);
  };

  const highlightMenu = () => {
      let navbar = document.getElementById('navbar');
      let scrollPos = window.scrollY;
      if (scrollPos < 100)
      {
        navbar.classList.add('navbar');
        navbar.classList.remove('navbarscrolled');
      } else {
        navbar.classList.remove('navbar');
        navbar.classList.add('navbarscrolled');
      }
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
.row {
    margin: 0 auto;
    max-width: 1200px;
}

/* Nav Bar Section */
.logo-box{
  display: flex;
}

.site-title-block{
  margin-left: 10px;
  color: #306eb9;
  text-align: left;
  line-height: 1px;
}

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

.navbarscrolled {
    box-shadow: inset 0 0 0 200px rgb(255 255 255 / 5%);
    backdrop-filter: blur(10px);
    background: rgba(255, 255, 255,0.8);
    height: 60px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 16px;
    position: sticky;
    top: 0;
    z-index: 999;
    transition: 0.5s linear all;
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
    background: #ffffff;
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

/* Main section */
.banner {
  width: 100%;
  height: auto;
}

ul.locations {
    margin: 0;
    list-style: none;
}

ul.locations li {
    float: left;
    width: 15%;
    height: auto;
    text-align: left;
    text-decoration: none;
}

a {
    text-decoration: none;
}

ul.locations .city-state {
    padding-top: 6px;
    font-weight: 700;
    font-size: 1.1em;
    line-height: 2em;
    color: orange
}

ul.locations .street-address {
    font-weight: 700;
    font-size: .8325em;
    color: #23384b;
}

input.search-input {
    border-color: gray;
    border-width: 1px;
    float: left;
    height: 40px;
    margin-right: 10px;
    background: #fff;
    font-size: 1.4em;
    font-family: Arial, sans-serif;
}

.search-btn{
  background-color: #306eb9;
  border-width: 0;
  color: #ffffff;
  height: 30px;
}

.location-box{
  height: 70px;
  position: relative;
}

.search-box {
  margin: 0;
  position: absolute;
  right: 0;
  top: 50%;
  transform: translateY(-50%);
}

.p-box{
  display: grid;
  grid-auto-flow: column;
  grid-column-gap: 20px;
  text-align: left;
}

.vertical-center{
  position: relative;
  top: 50%;
  -webkit-transform: translateY(-50%);
  -ms-transform: translateY(-50%);
  transform: translateY(-50%);
}

@media screen and (max-width: 715px) {
  .p-box{
    display: block;
  }
}

@media screen and (max-width: 915px) {
  .search-box{
    display: none;
  }

  ul.locations li {
    width: 25%;
  }
}

</style>

<!-- <button v-on:click="getEventData">Get events Data</button>
<div>{{eventDataList}}</div> -->