const tabs = [...document.querySelectorAll('.tabs>ul>li>a')];
const sections = document.querySelectorAll('.section');
const ACTIVETAB = 'active';
const ACTIVESECTION = 'activeSection';

sections[0].classList.add(ACTIVESECTION);

tabs.forEach(tab => {
  tab.addEventListener('click', () => {
    const sectionIndex = tabs.indexOf(tab);
    activateTab(tab);
    showSection(sections[sectionIndex]);
  });
});

function activateTab (selectedTab) {
  tabs.forEach(tab => {
    tab.classList.remove(ACTIVETAB);
  });
  selectedTab.classList.add(ACTIVETAB);
}

function showSection (selectedSection) {
  sections.forEach(section => {
    section.classList.remove(ACTIVESECTION);
  });
  selectedSection.classList.add(ACTIVESECTION);
}

const dropdownButton = document.querySelector('#dropdown');
const dropdownItems = document.querySelectorAll('.dropdown-item');
const currentPage = document.title.split(' ');
const buttonName = [];

let withinSeparator = false;

currentPage.forEach((word) => {
  if (withinSeparator === true) {
    buttonName.push(word);
  }
  if (word === '-') {
    if (withinSeparator === true) {
      buttonName.pop();
    }
    withinSeparator = !withinSeparator;
  }
});

dropdownButton.innerHTML = buttonName.join(' ');

dropdownItems.forEach((dropdownItem) => {
  dropdownItem.addEventListener('click', () => {
    dropdownButton.innerHTML = dropdownItem.innerHTML;
  });
});
