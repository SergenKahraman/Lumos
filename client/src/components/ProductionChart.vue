<template>
  <Bar :data="chartData" :options="chartOptions" class="h-100" />
</template>

<script setup>
import { Bar } from "vue-chartjs";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
} from "chart.js";

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
);

const chartData = {
  labels: ["GES-01 Ankara", "GES-02 İzmir", "GES-03 Antalya"],
  datasets: [
    {
      label: "Günlük Üretim (MWh)",
      data: [24.5, 18.2, 22.8],
      backgroundColor: "#2E5077",
      borderRadius: 6,
      barPercentage: 0.9,
    },
    {
      label: "Hedef Üretim (MWh)",
      data: [25.0, 20.0, 23.0],
      backgroundColor: "#5C89B5",
      borderRadius: 6,
      barPercentage: 0.9,
    },
  ],
};

const chartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  layout: {
    padding: {
      top: 20,
      bottom: 40,
      left: 20,
      right: 20,
    },
  },
  plugins: {
    legend: {
      position: "bottom",
      align: "center",
      labels: {
        boxWidth: 15,
        padding: 20,
        font: {
          size: 12,
        },
        usePointStyle: true,
        pointStyle: "circle",
      },
      margin: 20,
    },
    title: {
      display: false,
    },
  },
  scales: {
    y: {
      beginAtZero: true,
      grid: {
        display: true,
        drawBorder: false,
        color: "rgba(0, 0, 0, 0.05)",
      },
      ticks: {
        font: {
          size: 11,
        },
        padding: 8,
      },
    },
    x: {
      grid: {
        display: false,
      },
      ticks: {
        font: {
          size: 11,
        },
        padding: 8,
      },
    },
  },
  categoryPercentage: 0.9,
};
</script>

<style scoped>
.h-100 {
  height: 100%;
  padding-bottom: 16px;
}
</style>
