<template>
  <div class="container">
    <nav class="navbar navbar-expand-lg mb-4 border-bottom d-flex justify-content-between">
      <router-link :to="routes[0].path" class="navbar-brand d-flex align-items-center text-decoration-none">
        <a class="navbar-brand text-primary p-0">
          <span class="d-block">Progi Bid</span>
        </a>
      </router-link>
      <section>
        <a href="#" @click="(event) => changeTheme(event)" class="text-decoration-none">
          <div v-if="themeToggler" class="d-flex justify-content-between align-items-center">
            <small class="me-1">Light Up</small>
            <svg class="p-0" width="20" height="20" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
              <path
                d="M10 2a1 1 0 011 1v1a1 1 0 11-2 0V3a1 1 0 011-1zm4 8a4 4 0 11-8 0 4 4 0 018 0zm-.464 4.95l.707.707a1 1 0 001.414-1.414l-.707-.707a1 1 0 00-1.414 1.414zm2.12-10.607a1 1 0 010 1.414l-.706.707a1 1 0 11-1.414-1.414l.707-.707a1 1 0 011.414 0zM17 11a1 1 0 100-2h-1a1 1 0 100 2h1zm-7 4a1 1 0 011 1v1a1 1 0 11-2 0v-1a1 1 0 011-1zM5.05 6.464A1 1 0 106.465 5.05l-.708-.707a1 1 0 00-1.414 1.414l.707.707zm1.414 8.486l-.707.707a1 1 0 01-1.414-1.414l.707-.707a1 1 0 011.414 1.414zM4 11a1 1 0 100-2H3a1 1 0 000 2h1z"
                fill-rule="evenodd" clip-rule="evenodd"></path>
            </svg>
          </div>
          <div v-else class="d-flex justify-content-between align-items-center">
            <small class="me-1">Darken</small>
            <svg class="pa-0 ma-0" width="20" height="20" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
              <path d="M17.293 13.293A8 8 0 016.707 2.707a8.001 8.001 0 1010.586 10.586z"></path>
            </svg>
          </div>
        </a>
      </section>
    </nav>
  </div>
</template>

<script lang="ts" setup>
import { routes } from "@/router";
import { ref, type Ref } from "vue";

const themeToggler: Ref<boolean> = ref(false);
const docElm = document.documentElement;

const changeTheme = (e: Event) => {
  e.preventDefault();

  themeToggler.value = !themeToggler.value;
  docElm.dataset.bsTheme = themeToggler.value ? "dark" : "light";
};
</script>

<style lang="scss" scoped>
ul {
  list-style: none;
}
</style>
