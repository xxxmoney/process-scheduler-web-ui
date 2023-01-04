<template>
    <div class="grid grid-cols-2 gap-3">
        <Dropdown v-model="filter.Type" :options="types" optionLabel="label" optionValue="value" class="w-full col-span-2"></Dropdown>

        <div class="control-container">
            <label for="maxQueue">Number of frames</label>
            <InputNumber inputId="maxQueue" v-model="filter.MaxQueue" mode="decimal" showButtons :min="0" :max="100" class="min-w-0" inputClass="min-w-0" />
        </div>       
        <div class="control-container">
            <label for="maxQueue">Page references</label>
            <Chips id="pageReferences" v-model="filter.PageReferences" class="w-full" separator="," />
        </div>     

        <Button @click="submit" label="Submit" class="col-span-2"></Button>

        <div v-if="result" class="flex flex-row col-span-2">
            <div v-for="item in result.history" class="flex flex-col border border-gray-300 p-2">
                <div v-for="page in item" class="border border-gray-300 p-2">
                    {{ page?.pageNumber }}
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Dropdown from 'primevue/dropdown';
import Chips from 'primevue/chips';
import InputNumber from 'primevue/inputnumber';
import VirtualMemoryManagerType from '@/enums/VirtualMemoryManagerType';
import { computed, ref, watch } from 'vue';
import Button from 'primevue/button';
import { useToast } from 'primevue/usetoast';
import axios from 'axios'

export default {
    components: {
        Dropdown, Chips, InputNumber, Button
    },
    setup () {
        const types = [
            { label: 'FirstInFirstOut', value: VirtualMemoryManagerType.FirstInFirstOut },
            { label: 'LeastFrequentlyUsed', value: VirtualMemoryManagerType.LeastFrequentlyUsed },
            { label: 'LeastRecentlyUsed', value: VirtualMemoryManagerType.LeastRecentlyUsed },
            { label: 'Optimal', value: VirtualMemoryManagerType.Optimal },
            { label: 'SecondChance', value: VirtualMemoryManagerType.SecondChance },
        ];

        const filter = ref({
            Type: 0
        }); 
        const result = ref(null);
        
        const toast = useToast();
        const submit = async () => {            
            try {                
                const { data } = await axios.post('/api/solver/RunVirtualManager', filter.value);

                result.value = data;

                toast.add({severity:'success', summary: 'OK', detail:'', life: 1500});
            } catch (error) {
                console.error(error);

                toast.add({severity:'error', summary: 'Error', detail:'', life: 3000});
            }
        };

        // Watches for changes in the page references.
        watch(() => filter.value.PageReferences, () => {
            const filtered = filter.value.PageReferences
                .map(item => parseInt(item))
                .filter(item => !isNaN(item));

            // Checks whether there is a change.
            if (JSON.stringify(filtered) == JSON.stringify(filter.value.PageReferences)) {
                return;
            }
            
            filter.value.PageReferences = filtered;
        });

        const resultStyle = computed(() => 'display: grid; grid-template-columns: repeat(' + filter.value.MaxQueue + ', minmax(0, 1fr));');

        return {
            types,
            filter,
            result,
            resultStyle,
            submit
        }
    }
}
</script>

<style scoped>
    :deep(#pageReferences .p-inputtext) {
        width: 100% !important;
    }
</style>