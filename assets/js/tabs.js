const dropdownButton = document.querySelector("#dropdown");
const dropdownItems = document.querySelectorAll(".dropdown-item");
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
    withinSeparator = withinSeparator ? false : true;
  } 
});

dropdownButton.innerHTML = buttonName.join(' ');

dropdownItems.forEach((dropdownItem) => {
  dropdownItem.addEventListener("click", () => {
    dropdownButton.innerHTML = dropdownItem.innerHTML;
  });
});
