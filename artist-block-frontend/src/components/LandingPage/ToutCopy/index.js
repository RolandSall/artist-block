import React from "react";
import ReactDOM from "react-dom";
import Fade from "react-reveal/Fade";
import ToutText from "../ToutText";
import ToutHeader from "../ToutHeader";
import Cta from "../Cta";

class ToutCopy extends React.Component {
    render() {
        return (
            <div className="toutCopy w-full sm:w-1/2">
                <Fade bottom>
                    <ToutHeader />
                    <ToutText />
                    <Cta />
                </Fade>
            </div>
        );
    }
}

export default ToutCopy;
