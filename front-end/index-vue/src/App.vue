<template>
  <header>
    <link
      rel="stylesheet" href="https://use.fontawesome.com/releases/v5.14.0/css/all.css" integrity="sha384-HzLeBuhoNPvSl5KYnjx0BT+WB0QEEqLprO+NBkkk5gbc67FTaL7XIGa2w1L0Xbgc" crossorigin="anonymous"
    />
  </header>
  <nav id = "navbar" class="navbar">
      <div class="navbar__container">
        <div class ="logo-box">
          <img src="./assets/logo.png" style ="width: 60px;height: 60px;">
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
            <router-link to="/" class="navbar__links">Home</router-link>
          </li>
          <li class="navbar__item">
            <router-link to="/about" class="navbar__links">About us</router-link>
          </li>
          <li class="navbar__item">
            <a class="navbar__links">Get Involved</a>
          </li>
          <li class="navbar__item">
            <router-link to="/contact" class="navbar__links">Contact</router-link>
          </li>
          <li class="navbar__item">
            <router-link to="/resources" class="navbar__links">Resources</router-link>
          </li>
          <li class="navbar__item">
            <router-link to="/events" class="navbar__links">Events</router-link>
          </li>
          <li class="navbar__item">
            <router-link to="/news" class="navbar__links">Newsletter</router-link>
          </li>
        </ul>
      </div>
    </nav>
  <router-view/>
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
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: black;
}

/* Nav Bar Section */
.logo-box{
  display: flex;
}

.site-title-block{
  margin-left: 10px;
  color: #306eb9;
  text-align: left;
  overflow: hidden;
  display: -webkit-box;
  -webkit-line-clamp: 2; /* number of lines to show */
  -webkit-box-orient: vertical;
  max-height: 75px;
}

.site-title-block h2{
  line-height: 0;
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
  margin: 0 auto;
  max-width: 1200px;
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
    margin: 0 auto;
    max-width: 1200px;
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
</style>
