<!-- CustomDatatable.vue -->
<script setup>
import { computed, ref, useSlots } from 'vue'

const props = defineProps({
  itemsPerPage: { type: Number, default: 10 },
  groupByKey: { type: String, default: null },
  values: {
    type: Array,
    default: []
  },
});

const emits = defineEmits(["column-click", "row-click"]);
const slots = useSlots();

// Modificado para incluir o slot do corpo
const columns = computed(() => {
  const columnSlots = slots.default ? slots.default() : [];
  return columnSlots
    .filter(slot => slot.type && slot.type.__name === 'CustomColumn')
    .map(columnSlot => ({
      header: columnSlot.props?.header || '',
      accessorKey: columnSlot.props?.accessorKey || '',
      headerStyle: columnSlot.props?.headerStyle || {},
      columnStyle: columnSlot.props?.columnStyle || {},
      // Armazenar o slot de body da coluna
      bodySlot: columnSlot.children?.body
    }));
});

const data = computed(() => props.values);

const currentPage = ref(1);
const expandedGroups = ref({});

const groupedData = computed(() => {
  if (!props.groupByKey) return { '': data.value };

  return data.value.reduce((groups, item) => {
    const groupKey = item[props.groupByKey];
    if (!groups[groupKey]) groups[groupKey] = [];
    groups[groupKey].push(item);
    return groups;
  }, {});
});

const groupedKeys = computed(() => Object.keys(groupedData.value));

const totalPages = computed(() =>
  Math.ceil(groupedKeys.value.length / props.itemsPerPage)
);

const paginatedGroupedData = computed(() => {
  const start = (currentPage.value - 1) * props.itemsPerPage;
  const end = start + props.itemsPerPage;
  const visibleGroupKeys = groupedKeys.value.slice(start, end);

  return visibleGroupKeys.reduce((result, groupKey) => {
    result[groupKey] = groupedData.value[groupKey];
    return result;
  }, {});
});


const handleNextPage = () => {
  if (currentPage.value < totalPages.value) {
    currentPage.value++;
  }
};

const handlePreviousPage = () => {
  if (currentPage.value > 1) {
    currentPage.value--;
  }
};

const toggleGroup = (groupKey) => {
  expandedGroups.value[groupKey] = !expandedGroups.value[groupKey];
};


const handleRowClick = (item, column) => {
  emits("row-click", item, column);
}

const handleColumnClick = (item, column) => {
  emits("column-click", item, column);
}

</script>

<template>
  <div class="table-container">
    <table class="table">
      <thead>
        <slot name="header"></slot>
        <tr>
          <th v-for="column in columns" :key="column.accessorKey" :style="column.headerStyle">
            {{ column.header }}
          </th>
        </tr>
      </thead>
      <tbody>
        <template v-for="(groupItems, groupKey) in paginatedGroupedData">
          <tr class="group-row" @click="toggleGroup(groupKey)">
            <slot name="group-header" :isExpanded="expandedGroups[groupKey]" :group-key="groupKey" :items="groupItems">
              <td :colspan="columns.length">
                <div style="display: flex; align-items: center; cursor: pointer;">
                  <span style="margin-right: 8px;">
                    <span v-if="expandedGroups[groupKey]" class="pi pi-chevron-down"></span>
                    <span v-else class="pi pi-chevron-right"></span>
                  </span>
                  <strong>{{ groupKey }}</strong>
                </div>
              </td>
            </slot>
          </tr>

          <tr v-if="expandedGroups[groupKey]" v-for="item in groupItems" :key="item.id" @click="handleRowClick(item)">
            <td v-for="column in columns" :key="column.accessorKey" @click="handleColumnClick(item, column)"
              :style="column.columnStyle">
              <!-- Renderizar o slot de body específico da coluna se existir -->
              <component v-if="column.bodySlot" :is="column.bodySlot" :item="item" :value="item[column.accessorKey]" />
              <!-- Fallback para o valor padrão se não houver slot -->
              <template v-else>
                {{ item[column.accessorKey] }}
              </template>
            </td>
          </tr>

          <tr v-if="slots['group-footer'] && expandedGroups[groupKey]" class="group-row">
            <slot name="group-footer" :group-key="groupKey" :items="groupItems">
              <td :colspan="columns.length"></td>
            </slot>
          </tr>
        </template>

        <tr v-if="slots['row-expansion'] && groupItems.length > 0">
          <slot name="row-expansion" :group-key="groupKey" :items="groupItems"></slot>
        </tr>


      </tbody>

      <slot name="footer"></slot>
    </table>

    <div class="pagination">
      <button :disabled="currentPage === 1" @click="handlePreviousPage" class="pagination-button">
        Anterior
      </button>
      <span class="page-info">
        Página {{ currentPage }} de {{ totalPages }}
      </span>
      <button :disabled="currentPage === totalPages" @click="handleNextPage" class="pagination-button">
        Próxima
      </button>
    </div>
  </div>
</template>

<style scoped>
.table-container {
  width: 100%;
  margin: 20px 0;
}

.table {
  width: 100%;
  border-collapse: collapse;
  margin-bottom: 1rem;
}

:deep .table th,
:deep .table td {
  padding: 12px;
  border: 1px solid #ddd;
  text-align: left;
}

:deep .table th {
  background-color: #f5f5f5;
  font-weight: bold;
}

:deep .table tr:nth-child(even) {
  background-color: #f9f9f9;
}

:deep .table tr:hover {
  background-color: #f5f5f5;
}

.group-row {
  background-color: #e5e5e5;
  cursor: pointer;
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  margin-top: 1rem;
}

.pagination-button {
  padding: 8px 16px;
  border: 1px solid #ddd;
  background-color: #fff;
  cursor: pointer;
  border-radius: 4px;
}

.pagination-button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.page-info {
  font-size: 14px;
}
</style>
