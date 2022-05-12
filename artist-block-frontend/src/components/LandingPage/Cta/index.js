import React from "react";
import classnames from "classnames";

const Cta = props => (
    <div
        className={classnames(
            "cta text-center text-white w-auto relative inline-flex mt-12 p-4 px-8 cursor-pointer rounded-full",
            props.ctaBg
        )}
    >
        <a href="" className="font-bold">
            Link Text
        </a>
    </div>
);

export default Cta;
