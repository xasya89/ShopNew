
function set(key, value) { localStorage.setItem(key, value); }
function get(key) { return localStorage.getItem(key); }
function remove(key) { return localStorage.removeItem(key); }

function setSession(key, value) { sessionStorage.setItem(key, value); }
function getSession(key) { return sessionStorage.getItem(key); }
function removeSession(key) { return sessionStorage.removeItem(key); }