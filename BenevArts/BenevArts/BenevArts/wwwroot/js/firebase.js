// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyDQJqHIZvepPB7umJIKh0qy-p_G53-Aa20",
  authDomain: "benev-arts.firebaseapp.com",
  projectId: "benev-arts",
  storageBucket: "benev-arts.appspot.com",
  messagingSenderId: "218992334112",
  appId: "1:218992334112:web:8d96bfe46c9f679998b345",
  measurementId: "G-CQ6YHF95ZJ"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);