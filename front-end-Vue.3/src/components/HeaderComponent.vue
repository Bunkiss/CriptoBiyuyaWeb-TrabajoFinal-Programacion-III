<template>
  <header class="header">
    <div class="hamburger-menu">
      <button class="hamburger-buttom" @click.stop="toggleMenu">&#9776;</button> <!-- toggleMenu: función que alterna el valor de menuVisible.-->
      <nav class="hamburger-desplegado" v-show="menuVisible" @click.stop> <!-- v-show="menuVisible" en lugar de display: none -->
        <ul>
          <li>
            <RouterLink
              class="link"
              :to="{
                name: 'Transaction',
                query: { action: 'purchase' },
              }"
            >
              Nueva Compra
            </RouterLink>
          </li>
          <li>
            <RouterLink
              class="link"
              :to="{
                name: 'Transaction',
                query: { action: 'sale' },
              }"
            >
              Nueva Venta
            </RouterLink>
          </li>
          <li>
            <RouterLink
              class="link"
              to="/historial"
            >
              Historial de Movimientos
            </RouterLink>
          </li>
        </ul>
      </nav>
    </div>

    <div class="logo-menu">
      <router-link to="/" class="logo">Cripto Biyuya</router-link>
    </div>

    <div id="LoginIcon" class="fas fa-user">

    </div>
  </header>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue';
import { RouterLink } from 'vue-router';

const menuVisible = ref(false); // ref crea una referencia reactiva. Vue "reacciona" cuando menuVisible.value cambia.

const toggleMenu = () => {
  menuVisible.value = !menuVisible.value; //Invierte el valor: si está true, lo pone false, y viceversa.
};

const handleClickOutside = (e) => {
  const menu = document.querySelector('.hamburger-desplegado');
  const button = document.querySelector('.hamburger-buttom');

  if (menu && button && !menu.contains(e.target) && !button.contains(e.target)) { //menu.contains(e.target) verifica si el clic fue dentro del menú.
    menuVisible.value = false;
  }
}; //e.target: el elemento donde ocurrió el clic. Si el clic ocurre fuera del menú y del botón, se cierra el menú

onMounted(() => {
  document.addEventListener('click', handleClickOutside);
}); //onMounted: se ejecuta cuando el componente entra al DOM - empieza a escuchar clics globales.

onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside);
}); //onBeforeUnmount: se ejecuta justo antes de destruir el componente → elimina el event listener.
</script>


<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
  background-color: var(--bg-color);
  color: var(--text-secondary);
}

.hamburger-menu {
  position: relative;
}

.hamburger-buttom {
  font-size: 24px;
  background: none;
  border: none;
  color: var(--text-secondary);
  cursor: pointer;
}
.hamburger-buttom:hover {
  color: var(--success-color);
}

.hamburger-desplegado {
  position: absolute;
  top: 40px;
  background-color: var(--bg-color);
  padding: 10px;
  border-radius: 5px;
  z-index: 1000;
  box-shadow: 0 4px 8px rgba(0,0,0,0.5);
  width: 300px;
}

.hamburger-desplegado ul {
  list-style: none;
  margin: 0;
  padding: 0;
}
.hamburger-desplegado li {
  margin-bottom: 8px;
}
.hamburger-desplegado li .link {
  padding: 5px 10px;
  color: var(--text-secondary);
  text-decoration: none;
  font-weight: bold;
}
.hamburger-desplegado li .link:hover {
  background-color: var(--success-color);
  color: var(--bg-color);
  border-radius: 5px;
}

.logo-menu {
  display: flex;
  align-items: center;
  gap: 10px;
}

.logo {
  font-size: 25px;
  color: var(--ocaso-claro-1-color);
  text-decoration: none;
  font-weight: bold;
}
.logo:hover {
  color: var(--success-color);
}

#LoginIcon {
  padding-left: 10px;
  cursor: pointer;
  color: var(--text-secondary);
  font-size: x-large
}
#LoginIcon:hover {
  color: var(--success-color);
}
</style>
