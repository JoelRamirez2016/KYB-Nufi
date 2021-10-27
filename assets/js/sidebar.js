const sideBar = document.querySelector('.sidebar');
const mainContainer = document.querySelector('.main-container');
const menuButton = document.querySelector('.menu-btn');
const sideBarBack = document.querySelector('.sidebar-back');

menuButton.addEventListener('click', () => {
  if (sideBar.classList.contains('active')) {
    hideSideBar();
  } else {
    showSideBar();
  }
});

sideBarBack.addEventListener('click', () => {
  hideSideBar();
});

function hideSideBar () {
  sideBar.classList.remove('active');
  mainContainer.classList.remove('active');
  sideBarBack.classList.remove('active');
}
function showSideBar () {
  sideBar.classList.add('active');
  mainContainer.classList.add('active');
  sideBarBack.classList.add('active');
}
