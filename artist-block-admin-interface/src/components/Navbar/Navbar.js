import React from "react";
import { useNavigate } from "react-router-dom";
import View1 from "../Stats";

const Navbar = ({handleLogout}) => {
    let navigate = useNavigate();
    return (
        <section className="home">
            <nav>
                <h2 style={{color:"cornflowerblue"}}>Admin View</h2>
                <button onClick={handleLogout}>Logout</button>
            </nav>
          <View1/>
        </section>
    );
};

export default Navbar;
