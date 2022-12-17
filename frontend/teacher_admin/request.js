var XMLHttpRequest = require('xhr2');
const xhr = new XMLHttpRequest();



fetch("https://www.example.com/api/endpoint")
  .then(response => response.json())
  .then(data => console.log(data))
  .catch(error => console.error(error));