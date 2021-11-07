import React from "react";
import {Grid, IconButton, Typography} from "@mui/material";
import {NewTodo} from "./NewTodo";
import {Todos} from "./Todos";
import {TodoState} from "./Todo";
import DeleteIcon from "@mui/icons-material/Delete";
import {host, ITodo} from "../../../types";

interface ColumnProps {
    column_id: number
    name: string
    items: Array<ITodo>
    onClick: (index: number, action: number) => void
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

    handleNewTodoItemClick(name: string, description: string, deadline: Date, state: TodoState) {
        let position = this.state.items.length

        fetch(host + ":5000/api/todos", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                columnid: this.props.column_id,
                position: position,
                name: name,
                deadline: deadline,
                description: description,
                state: state
            })
        })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    items: this.state.items.concat({
                        id: data.id,
                        column_id: this.props.column_id,
                        position: position,
                        name: name,
                        deadline: deadline,
                        description: description,
                        state: state
                    })
                })
            })
            .catch(err => {
                console.log("Error while adding new Todo item: " + err)
            })
    }

    handleTodoItemClick(action: number, position: number, newData?: ITodo) {
        console.log("action: " + action + ", position: " + position)

        switch (action) {
            case 0: /* Moving Up */
                if(this.state.items[position].position > 0) {
                    fetch(host + ":5000/api/todos/swap", {
                        method: "PUT",
                        headers: {
                            'Content-type': 'application/json; charset=UTF-8'
                        },
                        body: JSON.stringify({
                            a: this.state.items[position].id,
                            b: this.state.items[position-1].id
                        })
                    })
                        .then(() => {
                            const newItemsList: Array<ITodo> = [] as ITodo[]

                            for(let i = 0; i < this.state.items.length; i++)
                                newItemsList.push(this.state.items[i])

                            newItemsList[position].position -= 1
                            newItemsList[position - 1].position += 1
                            newItemsList.sort((a: ITodo, b: ITodo) => (a.position > b.position) ? 1 : (b.position > a.position) ? -1 : 0)

                            this.setState({
                                items: newItemsList
                            })
                        })
                        .catch(err => {
                            console.log("Error when moving up Todo item: " + err)
                        })
                }
                break
            case 1: /* Moving Down */
                if(this.state.items[position].position < this.state.items.length - 1) {
                    fetch(host + ":5000/api/todos/swap", {
                        method: "PUT",
                        headers: {
                            'Content-type': 'application/json; charset=UTF-8'
                        },
                        body: JSON.stringify({
                            a: this.state.items[position].id,
                            b: this.state.items[position+1].id
                        })
                    })
                        .then(() => {
                            const newItemsList: Array<ITodo> = [] as ITodo[]

                            for(let i = 0; i < this.state.items.length; i++)
                                newItemsList.push(this.state.items[i])

                            newItemsList[position].position += 1
                            newItemsList[position + 1].position -= 1
                            newItemsList.sort((a: ITodo, b: ITodo) => (a.position > b.position) ? 1 : (b.position > a.position) ? -1 : 0)

                            this.setState({
                                items: newItemsList
                            })
                        })
                        .catch(err => {
                            console.log("Error when moving down Todo item: " + err)
                        })
                }
                break
            case 2: /* Deleting Item */
                fetch(host + ":5000/api/todos/" + this.state.items[position].id, {
                    method: "DELETE"
                })
                    .then(() => {
                        const newItemsList: Array<ITodo> = new Array<ITodo>()

                        for (let i = 0; i < this.state.items.length; i++)
                            if (this.state.items[i].position !== position) {
                                newItemsList.push(this.state.items[i])
                                if (this.state.items[i].position > position) newItemsList[newItemsList.length - 1].position -= 1
                            }

                        this.setState({
                            items: newItemsList
                        })
                    })
                    .catch(err => {
                        console.log("Error while deleting Todo item: " + err)
                    })
                break
            case 3: /* Saving Changed Item */
                if(newData !== undefined) {
                    fetch(host + ":5000/api/todos", {
                        method: "PUT",
                        headers: {
                            'Content-type': 'application/json; charset=UTF-8'
                        },
                        body: JSON.stringify({
                            id: newData.id,
                            columnid: newData.column_id,
                            position: newData.position,
                            name: newData.name,
                            description: newData.description,
                            deadline: newData.deadline,
                            state: newData.state
                        })
                    })
                        .then(() => {
                            const newItemsList: Array<ITodo> = new Array<ITodo>()

                            for (let i = 0; i < this.state.items.length; i++)
                                newItemsList.push(this.state.items[i])

                            newItemsList[newData?.position] = newData
                            this.setState({
                                items: newItemsList
                            })
                        })
                        .catch(err => {
                            console.log("Error while saving Todo item changes: " + err)
                        })
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
                        <IconButton color="secondary" onClick={() => this.props.onClick(this.props.column_id, 1)}>
                            <DeleteIcon />
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