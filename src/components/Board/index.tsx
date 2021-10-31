import React from "react";
import {Box, Button, Grid} from "@mui/material";
import {Column} from "./Column";
import AddIcon from '@mui/icons-material/Add';

class Board extends React.Component<any, any> {
    render() {
        return (
            <Grid container spacing={2} p={2}>
                <Column name="TODO"/>
                <Column name="In Progress"/>
                <Column name="Done"/>
                <Column name="Blocked"/>
                <Grid item>
                    <Box sx={{ mx: "auto", minWidth: 275, height: 100 }}
                         display="flex"
                         justifyContent="center"
                         alignItems="center">
                        <Button color="secondary" variant="contained" endIcon={<AddIcon />} fullWidth>
                            Új oszlop
                        </Button>
                    </Box>
                </Grid>
            </Grid>
        )
    }
}

export {Board}