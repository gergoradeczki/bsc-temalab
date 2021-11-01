import React from "react";
import {Box, Button, Grid} from "@mui/material";
import {Column} from "./Column";
import {Columns, ITodo, TodoItemsForColumns} from "../../mock";
import AddIcon from '@mui/icons-material/Add';

class Board extends React.Component<any, any> {

    todosForColumn(id: number): Array<ITodo> {
        let result = new Array<ITodo>()

        for(let item of TodoItemsForColumns)
            if(item.column_id === id)
                result.push(item)

        return result
    }

    render() {
        return (
            <Grid container spacing={2} p={2} justifyContent="center">
                {Columns.map((e, index) => (
                    <Column
                        column_id={e.id}
                        name={e.name}
                        items={this.todosForColumn(e.id)}
                        key={e.id}
                    />
                ))}
                <Grid item>
                    <Box sx={{ mx: "auto" }}
                         display="flex"
                         justifyContent="center"
                         alignItems="center">
                        <Button color="secondary" variant="contained" endIcon={<AddIcon />} fullWidth sx={{width: 275, height: 275}}>
                            Új oszlop
                        </Button>
                    </Box>
                </Grid>
            </Grid>
        )
    }
}

export {Board}