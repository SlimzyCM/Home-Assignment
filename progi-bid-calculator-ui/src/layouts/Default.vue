<template>
  <Nav />

  <main class="container mb-4">
    <BidCalculator></BidCalculator>
  </main>

  <footer class="container footer mb-2 mt-auto">
    <Footer />
  </footer>
</template>

<script lang="ts" setup>
import Nav from '@/components/Nav.vue'
import BidCalculator from '@/components/BidCalculator.vue'
import Footer from '@/components/Footer.vue'
</script>
