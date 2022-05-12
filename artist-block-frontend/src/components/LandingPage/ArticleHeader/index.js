import React, { Fragment } from "react";

class ArticleHeader extends React.Component {
    render() {
        return (
            <Fragment>
                <p className="article-intro w-4/5 text-xl sm:text-4xl">
                    Developing an intuitive system to control multiple in-vehicle systems.
                </p>
            </Fragment>
        );
    }
}

export default ArticleHeader;
