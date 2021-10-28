// Add the active class to tabSelected Tab
const tabs = document.querySelectorAll('.tabs>ul>li>a');
const tabSelected = document.querySelector('.tabs>ul').attributes.tabSelected.value;
tabs[tabSelected].classList.add('active');

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
