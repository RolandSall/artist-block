import React, {useEffect, useState} from "react";
import {hooks} from "../../query";

import "./styles.scss";
import {Avatar, Typography} from "@mui/material";
import {useHistory} from "react-router-dom";

const ListItem = ({onClick, title, icon, subtitle, isLoading = false}) => {

    return (
        <div onClick={onClick} className={`list-item-container ${isLoading && "loading"}`}>
            <div
                style={{
                    display: "flex",
                    flexDirection: "column",
                    placeContent: "center",
                }}
            >
                {
                    isLoading ?
                        <div
                            style={{
                                width: "35px",
                                height: "35px",
                                borderRadius: "100px",
                                display: "flex",
                                flexDirection: "column",
                                placeContent: "center",
                                backgroundColor: 'rgb(203 203 203)',
                                margin: "0 10px",
                            }}
                        />
                        :
                        icon ?
                            <div
                                style={{
                                    width: "35px",
                                    height: "35px",
                                    borderRadius: "100px",
                                    display: "flex",
                                    flexDirection: "column",
                                    placeContent: "center",
                                    backgroundImage: `url("${icon}")`,
                                    backgroundRepeat: "no-repeat",
                                    backgroundPosition: "center",
                                    margin: "0 10px",
                                    backgroundSize: "40px",
                                }}
                            />
                            :

                            <div
                                style={{
                                    width: "35px",
                                    height: "35px",
                                    borderRadius: "100px",
                                    display: "flex",
                                    flexDirection: "column",
                                    placeContent: "center",
                                    margin: "0 10px",
                                }}
                            >
                                <Avatar width={"35px"} height={"35px"}/>
                            </div>
                }

            </div>
            <div style={{
                flexGrow: 1,
                display: 'flex',
                flexDirection: 'column',
                placeContent: 'space-evenly',
            }}>
                <div style={{
                    display: 'flex'
                }}>
                    {
                        isLoading ?
                            <div style={{
                                borderRadius: '20px',
                                height: '15px',
                                width: '100%',
                                backgroundColor: 'rgb(203 203 203)'
                            }}/>
                            :
                            <Typography variant={"body"}>{title}</Typography>
                    }
                </div>
                {
                    !isLoading && subtitle &&

                    <div style={{
                        display: 'flex'
                    }}>

                        <Typography variant={"subtitle"}>{subtitle}</Typography>

                    </div>
                }
            </div>
        </div>
    );
};

const SearchPopover = ({search, searchSize, setOpen}) => {

    // const router = useRouter();

    const { data, isLoading, refetch, isRefetching } = hooks.useHomeSearch({ search })
    const [more, setMore] = useState({
        collectible: false,
        users: false,
        items: false
    })

    // const handleClickItem = (collectionAddress,tokenId) => {
    //     // if(from==="profile"){
    //     router.push({
    //         pathname: "/assets/[collectionAddress]/[tokenId]",
    //         query: {
    //             collectionAddress ,
    //             tokenId,
    //         },
    //     }).then(() => setOpen(false));        // }
    // };
    useEffect(() => {
        setOpen(true)
    }, [more])
    const {push} = useHistory()

    return (
        <div
            style={{
                display: "flex",
                background: "white",
                borderRadius: "10px",
                padding: "10px 0",
                boxShadow: "0 3px 30px 0 rgb(0 0 0 / 20%)",
                zIndex: "1000000000",
                width: searchSize ? searchSize.width : 'fit-content',
                maxHeight: '80vh',
                overflow: 'scroll',
                minWidth: '400px'
            }}
        >
            <div style={{padding: "0 20px", width: '100%'}}>
                <div className={"category"}>
                    <div>
                        <Typography variant={"title"}>Painters</Typography>
                    </div>
                    <div style={{margin: '10px 0'}}>
                        {
                            isLoading ?
                                <>
                                    {Array.from({length: 3}, (v, i, k) => i).map((e) => (
                                        < ListItem
                                            isLoading
                                        />
                                    ))
                                    }
                                </>
                                :
                                data?.painterList && data?.painterList.length===0 ?
                                    <>
                                        <Typography>
                                            No Painters Found
                                        </Typography>
                                    </>
                                    :
                                    <>
                                        <div style={{
                                            margin: '10px 0'
                                        }}>
                                            {
                                                data?.painterList?.map((e) =>
                                                    <ListItem
                                                        icon={e.painterUrl}
                                                        title={`${e.firstName} ${e.lastName}`}
                                                        // title={e.name}
                                                        onClick={() => push('../painter-profile/' + e.painterId)}
                                                        // onClick={() => handleClickItem(e.contractAddress,e.tokenId)}
                                                    />
                                                )
                                            }
                                        </div>
                                        {/*{*/}
                                        {/*    data[Object.keys(data).filter((e) => data[e].type === "items")]?.data.length > 3 &&*/}
                                        {/*    <div className={"more"}*/}
                                        {/*         onClick={() => setMore({...more, items: !more.items})}>*/}
                                        {/*        <Typography variant={"subtitle"}>*/}
                                        {/*            {more.items ? "Less" : "More"}*/}
                                        {/*        </Typography>*/}
                                        {/*    </div>*/}
                                        {/*}*/}

                                    </>

                        }
                    </div>

                </div>
                <div className={"category"}>
                    <div>
                        <Typography variant={"title"}>Paintings</Typography>
                    </div>
                    <div style={{margin: '10px 0'}}>
                        {
                            isLoading ?
                                <>
                                    {Array.from({length: 3}, (v, i, k) => i).map((e) => (
                                        < ListItem
                                            isLoading
                                        />
                                    ))
                                    }
                                </>
                                :
                                data?.paintingList && data?.paintingList.length===0 ?
                                    <>
                                        <Typography>
                                            No Paintings Found
                                        </Typography>
                                    </>
                                    :
                                    <>
                                        <div style={{
                                            margin: '10px 0'
                                        }}>
                                            {
                                                data?.paintingList?.map((e) =>
                                                    <ListItem
                                                        icon={e.paintingUrl}
                                                        title={`${e.paintingName}`}
                                                        subtitle={e.paintingYear}
                                                        //push('../painting/'+ e.paintingId)
                                                        onClick={() => push('../painting/'+ e.paintingId) }
                                                    />
                                                )
                                            }
                                        </div>
                                        {/*{*/}
                                        {/*    data[Object.keys(data).filter((e) => data[e].type === "items")]?.data.length > 3 &&*/}
                                        {/*    <div className={"more"}*/}
                                        {/*         onClick={() => setMore({...more, items: !more.items})}>*/}
                                        {/*        <Typography variant={"subtitle"}>*/}
                                        {/*            {more.items ? "Less" : "More"}*/}
                                        {/*        </Typography>*/}
                                        {/*    </div>*/}
                                        {/*}*/}

                                    </>

                        }
                    </div>

                </div>
            </div>
        </div>
    );
};

export default SearchPopover;
