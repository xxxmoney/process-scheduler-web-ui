<template>
    <div class="px-12 pb-12">
      <p class="text-6xl text-black font-bold">{{ $t("pages.home.main_heading") }}</p>
    </div>
    <div class="flex flex-col gap-7 px-12 py-9 bg-secondary my-text">
        <div>
          <img src="https://images.squarespace-cdn.com/content/v1/60f17919cd8d2e57b2d40714/1626695138614-H2ART1CPR9DAKF5EFQQN/sdas.PNG" alt="">
        </div>
        <p>{{ $t("pages.home.info.0") }}</p>
        <p>{{ $t("pages.home.info.1") }}</p>
    </div>
    <div class="px-12 pt-14">      
      <img src="https://images.squarespace-cdn.com/content/v1/60f17919cd8d2e57b2d40714/1626695394695-UAB56XS8PYZGTU2M72NI/yesss.PNG" alt="">            
      <h1 class="text-center text-4xl font-extrabold mt-5">{{ $t("pages.home.growth.heading") }}</h1>
      <div>
        <!-- Graph -->
      </div>  
                
      <div class="flex flex-col gap-7 pt-24">
        <div v-for="i in Array.from(Array(3).keys())" :key="i">
          <h1 class="text-xl font-extrabold pb-5">{{ $t(`pages.home.steps.${i}.heading`) }}</h1>
          <p class="font-semibold text-xl">{{ $t(`pages.home.steps.${i}.text`) }}</p>
        </div>
      </div>

      <img class="pt-8 pb-14 px-1" src="https://images.squarespace-cdn.com/content/v1/60f17919cd8d2e57b2d40714/1626694523435-UT93GDSVOIES4F7J4U13/jon-tyson-5KKglNl852A-unsplash.jpg" alt="">

      <div id="pricing" class="bg-secondary px-10 py-10 my-text">
        <div class="flex flex-col gap-10 text-center pb-8">
          <p>{{ $t("pages.home.pricing.top_text") }}</p>
          <h1 class="text-primary font-extrabold text-5xl">{{ $t("pages.home.pricing.heading") }}</h1>
          <h2 class="text-primary font-extrabold text-3xl">{{ $t("pages.home.pricing.sub_heading") }}</h2>
          <p class="italic text-2xl">{{ $t("pages.home.pricing.sub_text") }}</p>
        </div>

        <div class="flex flex-col gap-10 mb-12">
          <div v-for="i in Array.from(Array(3).keys())" :key="i">
            <h1 class="text-primary text-3xl mb-9">{{ $t(`pages.home.pricing.tiers.${i}.price`) }}</h1>
            <h2 class="text-primary text-xl font-extrabold mb-9">{{ $t(`pages.home.pricing.tiers.${i}.duration`) }}</h2>
            <p class="italic mb-11">{{ $t(`pages.home.pricing.tiers.${i}.per_month`) }}</p>
            <ul class="my-ul mb-14">
              <li v-for="j in Array.from(Array(7).keys())" :key="j">
                {{ $t(`pages.home.pricing.tiers.${i}.texts.${j}`) }}
              </li>
            </ul>
            <hr class="mt border-yellow-300">
          </div>
        </div>

        <div class="flex flex-col gap-9 text-center">
          <p class="text-2xl">{{ $t("pages.home.pricing.other_services") }}</p>
          <p class="text-2xl">{{ $t("pages.home.pricing.bottom_info.0") }}</p>
          <p class="text-lg">{{ $t("pages.home.pricing.bottom_info.1") }}</p>
          <h1 class="text-primary font-extrabold text-3xl">{{ $t("pages.home.pricing.payment_methods_heading") }}</h1>
          <ul class="my-ul px-14">
            <li class="" v-for="i in Array.from(Array(4).keys())" :key="i">
                {{ $t(`pages.home.pricing.payment_methods.${i}`) }}
            </li>
          </ul>
        </div>            
      </div>

      <div class="mt-16 px-2">
        <h1 class="font-extrabold text-4xl text-black mb-9">{{ $t("pages.home.contact.heading") }}</h1>
        <div class="flex flex-col gap-1 mb-8">
          <p class="font-semibold" v-for="i in Array.from(Array(3).keys())" :key="i">{{ $t(`pages.home.contact.contact_info.${i}`) }}</p>
        </div>
        <a href="" class="link">
          <font-awesome-icon :icon="['fab', 'instagram']" size="2x" />
        </a>  
        <div class="pt-7 w-full">              
          <!-- Calendily -->
        </div>     
        <div class="flex flex-row justify-center mb-10">
          <img src="https://images.squarespace-cdn.com/content/v1/60f17919cd8d2e57b2d40714/1626695674350-00RNSGJ0F8TK4GVJ2EO6/Capture.PNG" alt="">
        </div>
        <div class="flex flex-col gap-5">
          <p class="font-semibold" v-for="i in Array.from(Array(3).keys())" :key="i">{{ $t(`pages.home.bottom_info.${i}`) }}</p>
        </div>
      </div>
    </div>
</template>

<script>
  export default {
    name: 'Home'
  }
</script>
