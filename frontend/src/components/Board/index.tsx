import React from "react";
import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Grid,
    Stack,
    TextField
} from "@mui/material";
import {Column} from "./Column";
import {Columns, TodoItemsForColumns} from "../../mock";
import AddIcon from '@mui/icons-material/Add';
import {IColumn, ITodo} from "../../types";

interface BoardStates {
    columns: Array<IColumn>
    todos: Array<ITodo>
    cName: string
    isDialogOpen: boolean
}

class Board extends React.Component<any, BoardStates> {
    constructor(props: any) {
        super(props)
        this.findLargestIndex = this.findLargestIndex.bind(this)
        this.handleDialogOpen = this.handleDialogOpen.bind(this)
        this.handleDialogClose = this.handleDialogClose.bind(this)
        this.handleDialogAddBtn = this.handleDialogAddBtn.bind(this)
        this.state = {
            columns: Columns,
            todos: TodoItemsForColumns,
            cName: "",
            isDialogOpen: false
        }
    }

    findLargestIndex(): number {
        let result = 0
        for(let column of this.state.columns)
            if(column.id > result)
                result = column.id
        return result;
    }

    todosForColumn(id: number): Array<ITodo> {
        let result = new Array<ITodo>()

        for(let item of this.state.todos)
            if(item.column_id === id)
                result.push(item)

        return result
    }

    handleDialogClose() {
        this.setState({
            isDialogOpen: false
        });
    }

    handleDialogOpen() {
        this.setState({
            isDialogOpen: true,
            cName: ""
        })
    }

    handleDialogAddBtn() {
        let newColumnList = new Array<IColumn>()
        for(let column of this.state.columns)
            newColumnList.push(column)

        newColumnList.push({id: this.findLargestIndex() + 1, name: this.state.cName})

        // TODO: API hívás ide

        this.setState({
            columns: newColumnList
        })
        this.handleDialogClose()
    }

    handleColumnOnClick(index: number, action: number) {
        switch (action) {
            case 0:
                break
            case 1:
            {
                let newColumnList = new Array<IColumn>()
                for(let column of this.state.columns)
                    if(column.id !== index)
                        newColumnList.push(column)

                let newTodoList = new Array<ITodo>()
                for(let todo of this.state.todos)
                    if(todo.column_id !== index)
                        newTodoList.push(todo)
                
                // TODO: API hívás ide

                this.setState({
                    columns: newColumnList,
                    todos: newTodoList
                })

                break
            }
        }
    }

    render() {
        return (
            <Grid container spacing={2} p={2} justifyContent="center">
                {this.state.columns.map((e: IColumn, index: number) => (
                    <Column
                        column_id={e.id}
                        name={e.name}
                        items={this.todosForColumn(e.id)}
                        key={e.id}
                        onClick={(index: number, action: number) => this.handleColumnOnClick(index, action)}
                    />
                ))}
                <Grid item>
                    <Box sx={{ mx: "auto" }}
                         display="flex"
                         justifyContent="center"
                         alignItems="center">
                        <Button color="secondary" variant="contained" endIcon={<AddIcon />} fullWidth sx={{width: 275, height: 275}}
                                onClick={this.handleDialogOpen}>
                            Új oszlop
                        </Button>
                        <Dialog open={this.state.isDialogOpen} onClose={this.handleDialogClose}>
                            <DialogTitle>Új oszlop felvétele</DialogTitle>
                            <DialogContent>
                                <Box component="form" sx={{'& > :not(style)': { m: 1, width: '25ch' },}}>
                                    <Stack spacing={2}>
                                        <TextField
                                            id="column-name"
                                            label="Név"
                                            variant="outlined"
                                            InputLabelProps={{
                                                shrink: true,
                                            }}
                                            onChange={(e) => this.setState({cName: e.target.value})}
                                        />
                                    </Stack>
                                </Box>
                            </DialogContent>
                            <DialogActions>
                                <Button color="secondary" onClick={this.handleDialogClose}>Mégsem</Button>
                                <Button color="primary" variant="contained" onClick={this.handleDialogAddBtn}>Hozzáadás</Button>
                            </DialogActions>
                        </Dialog>
                    </Box>
                </Grid>
            </Grid>
        )
    }
}

export {Board}