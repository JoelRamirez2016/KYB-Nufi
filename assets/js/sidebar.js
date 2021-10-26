const sideBar = document.querySelector(".sidebar");
const mainContainer = document.querySelector(".main-container");
const menuButton = document.querySelector(".menu-btn");

menuButton.addEventListener("click", () => {
  if (sideBar.classList.contains("active")) {
    sideBar.classList.remove("active");
    mainContainer.classList.remove("active");
  } else {
    sideBar.classList.add("active");
    mainContainer.classList.add("active");
  }
});
