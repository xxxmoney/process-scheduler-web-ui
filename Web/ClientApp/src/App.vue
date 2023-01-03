<template>
  <header class="sticky top-0 left-0 w-full px-4 py-5 grid grid-cols-3 bg-secondary text-primary">
    <nav-menu></nav-menu>
    <h1 class="big-heading flex items-center">
      <a href="/">Follow Agency</a>
    </h1>
    <LocaleChanger class="flex flex-row justify-end items-center" />
  </header>

  <main class="bg-primary py-12">
    <router-view />
  </main>
    
  <footer class="bg-secondary text-primary px-12 pt-12 pb-24">
    <BottomFooter />
  </footer>
</template>

<script>
  import NavMenu from './components/NavMenu.vue'
  import LocaleChanger from './components/LocaleChanger.vue'
  import BottomFooter from './components/BottomFooter.vue'

  export default {
    name: 'App',
    components: {
        NavMenu,
        LocaleChanger,
        BottomFooter
    }
  }
  
</script>

<style>
  body {
    margin: 0;
  } 

  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    color: #2c3e50;
  }

</style>
