<template>
  <div id="notfound">
    <div class="notfound">
      <div class="notfound-404">
        <h1>Oops!</h1>
      </div>
      <h2>404 - Page not found</h2>
      <p>
        The page you are looking for might have been removed had its name
        changed or is temporarily unavailable.
      </p>
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
