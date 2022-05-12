import React from "react";
import classnames from "classnames";

const Pullout = props => (
    <div className={classnames("pullout w-full", props.pulloutBackground)}>
        <div className="pullout-content relative flex flex-col sm:flex-row w-11/12 sm:w-10/12 ml-auto">
            <div className="pullout-copy w-full sm:w-1/2 block sm:inline-block">
                <h2 className="pullout-header w-11/12 sm:w-10/12 text-3xl sm:text-4xl leading-tight">
                    Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis
                    suscipit.
                </h2>
                <div className="pullout-subheading ml-auto text-base pt-12 pb-0 sm:pb-16 pr-4 sm:pr-10 w-10/12 sm:w-7/12">
                    Sed ut perspiciatis unde omnis iste natus error sit voluptatem
                    accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae
                    ab illo inventore veritatis et quasi architecto beatae vitae dicta
                    sunt explicabo.
                </div>
            </div>
            <div className="pullout-image relative sm:absolute right-0 w-11/12 sm:w-1/2 block sm:inline-block" />
        </div>
    </div>
);

export default Pullout;
