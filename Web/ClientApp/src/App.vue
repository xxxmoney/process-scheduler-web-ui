<template>
  <Toast />

  <div class="lg:w-1/2 xl:w-1/3 py-16 px-2 lg:px-8">
    <Dropdown v-model="selected" :options="types" optionLabel="label" optionValue="value" placeholder="Select a type" class="w-full"></Dropdown>

    <VirtualMemoryManager v-if="selected == SolverType.VirtualMemoryManager" />
    <ProcessSCheduler v-if="selected == SolverType.ProcessScheduler" />
  </div>
</template>

<script>
import Dropdown from 'primevue/dropdown';
import { ref } from 'vue';
import VirtualMemoryManager from './components/VirtualMemoryManager.vue';
import ProcessSCheduler from './components/ProcessScheduler.vue';
import SolverType from './enums/SolverType';
import Toast from 'primevue/toast';

export default {
  name: 'App',
  components: {
    Dropdown, 
    VirtualMemoryManager, 
    ProcessSCheduler, 
    Toast
  },
  setup() {
    const types = [
      { label: 'ProcessSheduler', value: SolverType.ProcessScheduler },
      { label: 'VirtualMemoryManager', value: SolverType.VirtualMemoryManager },
    ];

    const selected = ref();

    // Hacky fix for transparent buttons.
    setInterval(() => {
        // Sets type to empty.
        document.querySelectorAll("button[type='button']").forEach(el => {
          el.setAttribute('type', '');
        });
      }, 50);

    return {
      selected,
      types,
      SolverType
    }
  }
}
</script>

<style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    color: #2c3e50;
  }

  
</style>

