const tabs = [...document.querySelectorAll('.tabs>ul>li>a')];
const sections = document.querySelectorAll('.section');
const dropdownButton = document.querySelector('#dropdown');
const dropdownItems = [...document.querySelectorAll('.dropdown-item')];
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

dropdownItems.forEach(dItem => {
  dItem.addEventListener('click', () => {
    const sectionIndex = dropdownItems.indexOf(dItem);
    console.log(sectionIndex);
    showSection(sections[sectionIndex]);
  });
});

dropdownButton.innerHTML = 'General';

dropdownItems.forEach((dropdownItem) => {
  dropdownItem.addEventListener('click', () => {
    dropdownButton.innerHTML = dropdownItem.innerHTML;
  });
});
