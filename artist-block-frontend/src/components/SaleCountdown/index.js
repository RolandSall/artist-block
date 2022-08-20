import React, { useEffect, useState } from "react";
import Countdown from "react-countdown";
import {Grid, Typography} from "@mui/material";
import moment from "moment";
import {ARTIST_PINK} from "../../utils/constants";


const SaleCountdown = ({ time }) => {
    // Renderer callback with conditio

    console.log(time)
    console.log(moment(time).unix())
    const [countdown, setCountTime] = useState(parseInt(moment(time).format('x')));

    const saleWillFinish = ({
                                formatted: { days, hours, minutes, seconds },
                                completed,
                            }) => {
            return (
                <div
                >
                    <Grid container>
                            {days !== "00" && (
                                <Grid item xs>
                                    <Typography textAlign={"center"} 
                                        variant="h6"
                                        fontSize="25px"
                                    >
                                        {days}
                                    </Typography>
                                </Grid>
                            )}
                            <Grid item xs>
                                <Typography textAlign={"center"} 
                                    variant="h6"
                                    fontSize="25px"
                                >
                                    {hours}
                                </Typography>
                            </Grid>
                            <Grid item xs>
                                <Typography textAlign={"center"} 
                                    variant="h6"
                                    fontSize="25px"
                                >
                                    {minutes}
                                </Typography>
                            </Grid>
                            <Grid item xs>
                                <Typography textAlign={"center"} 
                                    variant="h6"
                                    fontSize="25px"
                                >
                                    {seconds}
                                </Typography>
                            </Grid>
                        </Grid>
                    <Grid container>
                        {days !== "00" && (
                            <Grid item xs>
                                <Typography textAlign={"center"} >DD</Typography>
                            </Grid>
                        )}
                        <Grid item xs>
                            <Typography textAlign={"center"} >HH</Typography>
                        </Grid>
                        <Grid item xs>
                            <Typography textAlign={"center"}  >MM</Typography>
                        </Grid>
                        <Grid item xs>
                            <Typography textAlign={"center"} >SS</Typography>
                        </Grid>
                    </Grid>
                </div>
            );
        }

    return <Countdown date={countdown} renderer={saleWillFinish} />;
};

export default SaleCountdown;
