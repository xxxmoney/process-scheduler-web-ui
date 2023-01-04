<template>
    <div class="flex flex-col gap-3">
        <div class="grid grid-cols-1 flex-grow gap-3">
            <Dropdown
              v-model="filter.Type"
              :options="types"
              optionLabel="label"
              optionValue="value"
              class="w-full"></Dropdown>
    
            <div class="control-container" v-if="filter.Type == ProcessSchedulerType.Priority">
                <label for="maxQueue">Preemptive</label>
                <InputSwitch
                  v-model="filter.Preemptive" />
            </div>  

            <div class="control-container" v-if="filter.Type == ProcessSchedulerType.RoundRobin">
                <label>Time Slice</label>
                <InputNumber
                  v-model="filter.TimeSlice"
                  mode="decimal"
                  showButtons
                  :min="0"
                  :max="100"
                  class="min-w-0"
                  inputClass="min-w-0" />
            </div> 
    
            <div class="flex flex-col gap-3">
                <div class="flex flex-col" v-for="(process, index) in filter.Processes">                    
                    <div class="grid grid-cols-1 gap-2" :class="filter.Type == ProcessSchedulerType.Priority ? 'lg:grid-cols-9' : 'lg:grid-cols-7'">
                        <div class="flex flex-col gap-2 justify-end items-start col-span-1">                    
                            <Button
                              icon="pi pi-minus"
                              class="p-button p-button-danger w-full"
                              @click="removeProcess(index)" />
                        </div>
                        <div class="control-container lg:col-span-2">
                            <label>Process Id</label>
                            <InputNumber
                              v-model="process.ProcessId"
                              mode="decimal"
                              showButtons
                              :min="0"
                              :max="100"
                              class="min-w-0"
                              inputClass="min-w-0" />
                        </div>  
    
                        <div class="control-container lg:col-span-2">
                            <label>Arrival Time</label>
                            <InputNumber
                              v-model="process.ArrivalTime"
                              mode="decimal"
                              showButtons
                              :min="0"
                              :max="100"
                              class="min-w-0"
                              inputClass="min-w-0" />
                        </div>  
    
                        <div class="control-container lg:col-span-2">
                            <label>Burst Time</label>
                            <InputNumber
                              v-model="process.BurstTime"
                              mode="decimal"
                              showButtons
                              :min="0"
                              :max="100"
                              class="min-w-0"
                              inputClass="min-w-0" />
                        </div> 
                        
                        <div class="control-container lg:col-span-2" v-if="filter.Type == ProcessSchedulerType.Priority">
                            <label>Priority</label>
                            <InputNumber
                              v-model="process.Priority"
                              mode="decimal"
                              showButtons
                              :min="0"
                              :max="100"
                              class="min-w-0"
                              inputClass="min-w-0" />
                        </div>  

                        <Inplace
                          :closable="true"
                          class="lg:col-span-4"
                          :class="filter.Type == ProcessSchedulerType.Priority ? 'lg:col-start-6' : 'lg:col-start-4'"
                          @open="initializeInterruption(process)"
                          @close="removeInterruption(process)">
                            <template #display>
                                <span>Add Interruption</span>
                            </template>
                            <template #content>
                                <div class="grid grid-cols-1 lg:grid-cols-2 gap-2">
                                    <div class="control-container">
                                        <label>Limit</label>
                                        <InputNumber
                                          v-model="process.Interruption.Limit"
                                          mode="decimal"
                                          showButtons
                                          :min="0"
                                          :max="100"
                                          class="min-w-0"
                                          inputClass="min-w-0" />
                                    </div>  
                                    <div class="control-container">
                                        <label>Wait Limit</label>
                                        <InputNumber
                                          v-model="process.Interruption.WaitTime"
                                          mode="decimal"
                                          showButtons
                                          :min="0"
                                          :max="100"
                                          class="min-w-0"
                                          inputClass="min-w-0" />
                                    </div> 
                                </div>
                            </template>
                        </Inplace>
                    </div>
                </div>
    
                <Button icon="pi pi-plus" class="p-button w-full full-icon" @click="addProcess" />            
            </div>
    
            <Button @click="submit" label="Submit" class=""></Button>
        </div>
        
        <div class="">
            <div v-if="result" class="flex flex-col">
                <DataTable :value="result.history" responsiveLayout="scroll">
                    <Column field="time" header="Time"></Column>
                    <Column field="processId" header="ProcessId"></Column>
                </DataTable>
            </div>
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
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Inplace from 'primevue/inplace';
import axios from 'axios'
import { useToast } from 'primevue/usetoast';

export default {
    components: {
        Dropdown, InputNumber, Button, InputSwitch, DataTable, Column, Inplace
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
        const initializeInterruption = (process) => {        
            if (process.Interruption == null) {
                process.Interruption = {
                    Limit: 0,
                    WaitTime: 0
                };
            }
        };
        const removeInterruption = (process) => {
            process.Interruption = null;
        };
        const addProcess = () => {
            filter.value.Processes.push({
                ProcessId: filter.value.Processes[filter.value.Processes.length - 1]?.ProcessId + 1 || 1,
                ArrivalTime: 0,
                BurstTime: 0,
                Priority: null,
                Interruption: null
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
            ProcessSchedulerType,
            types,
            filter,
            result,
            submit,
            addProcess,
            removeProcess,
            initializeInterruption,
            removeInterruption
        }
    }
}
</script>

<style scoped>
    :deep(.full-icon .p-button-icon-only) {
        width: 100% !important;
    }
</style>