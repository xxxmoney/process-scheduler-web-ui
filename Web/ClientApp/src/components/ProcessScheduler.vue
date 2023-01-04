<template>
    <div class="grid grid-cols-1 gap-3">
        <Dropdown v-model="filter.Type" :options="types" optionLabel="label" optionValue="value" class="w-full"></Dropdown>

        <div class="control-container">
            <label for="maxQueue">Preemptive</label>
            <InputSwitch v-model="filter.Preemptive" />
        </div>  

        <div class="flex flex-col gap-3">
            <div class="mb-2" v-for="(process, index) in filter.Processes">
                
                <div class="grid grid-cols-1 gap-2 lg:grid-cols-10">
                    <div class="flex flex-col justify-end items-start col-span-1">                    
                        <Button icon="pi pi-minus" class="p-button p-button-danger w-full" @click="removeProcess(index)" />     
                    </div>
                    <div class="control-container lg:col-span-3">
                        <label>Process Id</label>
                        <InputNumber v-model="process.ProcessId" mode="decimal" showButtons :min="0" :max="100" class="min-w-0" inputClass="min-w-0" />
                    </div>  

                    <div class="control-container lg:col-span-3">
                        <label>Arrival Time</label>
                        <InputNumber v-model="process.ArrivalTime" mode="decimal" showButtons :min="0" :max="100" class="min-w-0" inputClass="min-w-0" />
                    </div>  

                    <div class="control-container lg:col-span-3">
                        <label>Burst Time</label>
                        <InputNumber v-model="process.BurstTime" mode="decimal" showButtons :min="0" :max="100" class="min-w-0" inputClass="min-w-0" />
                    </div>                  
                </div>
            </div>

            <Button icon="pi pi-plus" class="p-button w-full full-icon" @click="addProcess" />            
        </div>

        <Button @click="submit" label="Submit" class=""></Button>

        <div v-if="result" class="">

        </div>
    </div>
</template>

<script>
import Dropdown from 'primevue/dropdown';
import InputNumber from 'primevue/inputnumber';
import { ref } from 'vue';
import Button from 'primevue/button';
import ProcessSchedulerType from '@/enums/ProcessSchedulerType';
import InputSwitch from 'primevue/inputswitch';
import axios from 'axios'
import { useToast } from 'primevue/usetoast';

export default {
    components: {
        Dropdown, InputNumber, Button, InputSwitch
    },
    setup () {
        const types = [
            { label: 'FirstComeFirstServe', value: ProcessSchedulerType.FirstComeFirstServe },
            { label: 'Priority', value: ProcessSchedulerType.Priority },
            { label: 'RoundRobin', value: ProcessSchedulerType.RoundRobin },
            { label: 'ShortestJobFirst', value: ProcessSchedulerType.ShortestJobFirst },
            { label: 'ShortestRemainingTimeFirst', value: ProcessSchedulerType.ShortestRemainingTimeFirst },
        ];
        const filter = ref({
            Type: 0,
            Processes: []
        });
        const addProcess = () => {
            filter.value.Processes.push({
                ProcessId: filter.value.Processes[filter.value.Processes.length - 1]?.ProcessId + 1 || 1,
                ArrivalTime: 0,
                BurstTime: 0
            });
        };
        const removeProcess = (index) => {
            filter.value.Processes.splice(index, 1);
        }

        const result = ref();

        const toast = useToast();
        const submit = async () => {
            try {
                const { data } = await axios.post('/api/solver/RunProcessScheduler', filter.value);

                result.value = data;

                toast.add({severity:'success', summary: 'OK', detail:'', life: 1500});
            } catch (error) {
                console.error(error);

                toast.add({severity:'error', summary: 'Error', detail:'', life: 3000});
            }            
        };

        return {
            types,
            filter,
            result,
            submit,
            addProcess,
            removeProcess
        }
    }
}
</script>

<style scoped>
    :deep(.full-icon .p-button-icon-only) {
        width: 100% !important;
    }
</style>