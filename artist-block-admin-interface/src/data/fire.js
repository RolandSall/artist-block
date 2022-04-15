// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyB-NDJR_QTrUBFK9k7OKitcSAZ62dO-xa0",
    authDomain: "artist-block-admin-page.firebaseapp.com",
    projectId: "artist-block-admin-page",
    storageBucket: "artist-block-admin-page.appspot.com",
    messagingSenderId: "583444312087",
    appId: "1:583444312087:web:b686dad0d4155bca07ec52",
    measurementId: "G-QNEB7PMPKV"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);

export default app;
