<template>
  <div id="notfound">
    <div class="notfound">
      <div class="notfound-404">
        <h1>Oops!</h1>
      </div>
      <h2 class="text-danger">
        An error occurred while processing your request !!!
      </h2>
      <p>Please refresh the page or try after some time.</p>
      <router-link :to="routes[0].path">
        <a>Go To Homepage </a>
      </router-link>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { routes } from '@/router'
</script>

<style scoped></style>
