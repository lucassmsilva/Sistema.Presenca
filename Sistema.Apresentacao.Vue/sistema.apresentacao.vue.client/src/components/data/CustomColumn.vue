<!-- CustomColumn.vue -->
<script setup>
defineProps({
  header: {
    type: String,
    required: true
  },
  accessorKey: {
    type: String,
    required: true
  },
  headerStyle: {
    type: Object,
    default: () => ({})
  },
  columnStyle: {
    type: Object,
    default: () => ({})
  },
  expanded: {
    type: Boolean,
    required: true
  },
})
</script>

<template>
  <slot></slot>
</template>