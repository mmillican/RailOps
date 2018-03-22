
<template>
  <div id="app">
    <header>
        <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
            <div class="container">
              <a href="#" class="navbar-brand">Rail Ops</a>
            
              <button class="navbar-toggler d-lg-none" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
                  <span class="navbar-toggler-icon"></span>
              </button>

              <div class="collapse navbar-collapse" id="navbarsExampleDefault">
                  <ul class="navbar-nav mr-auto">
                      <li class="nav-item">
                          <router-link to="/" class="nav-link">Home</router-link>
                      </li>
                      <li class="nav-item">
                          <router-link to="/roster/roads" class="nav-link" >Roads</router-link>
                      </li>
                  </ul>
                  <!-- <form class="form-inline mt-2 mt-md-0">
                      <input class="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search">
                      <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                  </form> -->
                </div>
            </div>
        </nav>
    </header>
      
    <div class="container body-content">
      <router-view/>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'App'
  }
</script>

<style>
.body-content { padding-top: 60px; }

/*
 * Sidebar
 */

 .sidebar {
    position: fixed;
    top: 51px;
    bottom: 0;
    left: 0;
    z-index: 1000;
    padding: 20px 0;
    overflow-x: hidden;
    overflow-y: auto; /* Scrollable contents if viewport is shorter than content. */
    border-right: 1px solid #eee;
  }
  
  .sidebar .nav {
    margin-bottom: 20px;
  }
  
  .sidebar .nav-item {
    width: 100%;
  }
  
  .sidebar .nav-item + .nav-item {
    margin-left: 0;
  }
  
  .sidebar .nav-link {
    border-radius: 0;
  }
</style>
