import React from "react";
import {Grid, IconButton, Typography} from "@mui/material";
import {NewTodo} from "./NewTodo";
import {Todos} from "./Todos";
import {TodoState} from "./Todo";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { ITodo } from "../../../types";

interface ColumnProps {
    column_id: number
    name: string
    items: Array<ITodo>
}

interface ColumnStates {
    name: string
    items: Array<ITodo>
}

class Column extends React.Component<ColumnProps, ColumnStates> {
    constructor(props: ColumnProps) {
        super(props);
        this.state = {
            name: this.props.name,
            items: this.props.items
        }
    }

    findLargestIndex() : number {
        let max = 0
        for(let i = 0; i < this.state.items.length; i++) {
            max = (this.state.items[i].id > max) ? this.state.items[i].id : max
        }
        return max;
    }

    handleNewTodoItemClick(name: string, description: string, deadline: Date, state: TodoState) {
        let id = this.findLargestIndex() + 1
        let position = this.state.items.length
        this.setState({
            items: this.state.items.concat({
                id: id,
                column_id: this.props.column_id,
                position: position,
                name: name,
                deadline: deadline,
                description: description,
                state: state
            })
        })
    }

    handleTodoItemClick(action: number, position: number, newData?: ITodo) {
        console.log("action: " + action + ", position: " + position)

        switch (action) {
            case 0: /* Moving Up */
                if(this.state.items[position].position > 0) {
                    const newItemsList: Array<ITodo> = [] as ITodo[]

                    for(let i = 0; i < this.state.items.length; i++)
                        newItemsList.push(this.state.items[i])

                    newItemsList[position].position -= 1
                    newItemsList[position - 1].position += 1
                    newItemsList.sort((a: ITodo, b: ITodo) => (a.position > b.position) ? 1 : (b.position > a.position) ? -1 : 0)

                    this.setState({
                        items: newItemsList
                    })
                }
                /* TODO: API hívás ide */
                break
            case 1: /* Moving Down */
                if(this.state.items[position].position < this.state.items.length - 1) {
                    const newItemsList: Array<ITodo> = [] as ITodo[]

                    for(let i = 0; i < this.state.items.length; i++)
                        newItemsList.push(this.state.items[i])

                    newItemsList[position].position += 1
                    newItemsList[position + 1].position -= 1
                    newItemsList.sort((a: ITodo, b: ITodo) => (a.position > b.position) ? 1 : (b.position > a.position) ? -1 : 0)

                    this.setState({
                        items: newItemsList
                    })
                }
                /* TODO: API hívás ide */
                break
            case 2: /* Deleting Item */
                {
                    const newItemsList: Array<ITodo> = new Array<ITodo>()

                    for (let i = 0; i < this.state.items.length; i++)
                        if (this.state.items[i].position !== position) {
                            newItemsList.push(this.state.items[i])
                            if (this.state.items[i].position > position) newItemsList[newItemsList.length - 1].position -= 1
                        }

                    this.setState({
                        items: newItemsList
                    })
                    /* TODO: API hívás ide */
                }
                break
            case 3: /* Saving Changed Item */
                {
                    const newItemsList: Array<ITodo> = new Array<ITodo>()

                    for (let i = 0; i < this.state.items.length; i++)
                        newItemsList.push(this.state.items[i])

                    if(newData !== undefined) {
                        newItemsList[newData?.position] = newData
                        this.setState({
                            items: newItemsList
                        })
                    }
                    /* TODO: API hívás ide */
                }
                break
        }
    }

    render() {
        return (
            <>
                <Grid item>
                    <Typography align="center" variant={"h6"} p={2}>
                        {this.props.name}
                        <IconButton color="secondary">
                            <EditIcon/>
                        </IconButton>
                        <IconButton color="secondary">
                            <DeleteIcon/>
                        </IconButton>
                    </Typography>
                    <NewTodo onClick={(name: string, desc: string, deadline: Date, state: TodoState) => this.handleNewTodoItemClick(name, desc, deadline, state)}/>
                    <Todos items={this.state.items} onClick={(action: number, position: number, newData?: ITodo) => this.handleTodoItemClick(action, position, newData)}/>
                </Grid>
            </>
        )
    }
}

export {Column}