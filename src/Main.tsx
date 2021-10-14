import React from "react";
import {Grid, Paper} from "@mui/material";
import {NewTodo} from "./components/NewTodo";
import {ITodo, TodoItems} from "./mock";
import {Todo} from "./components/Todo";
import {Todos} from "./components/Todos";

interface MainStates {
    items: any
}

export default class Main extends React.Component<any, MainStates> {
    constructor(props: any) {
        super(props);
        this.state = {
            items: TodoItems
        }
    }

    findLargestIndex() : number {
        let max = 0
        for(let i = 0; i < this.state.items.length; i++) {
            max = (this.state.items[i].id > max) ? this.state.items[i].id : max
        }
        return max;
    }

    handleNewTodoItemClick(name: string, description: string) {
        let id = this.findLargestIndex() + 1
        let position = this.state.items.length
        let date = new Date()
        this.setState({
            items: this.state.items.concat({
                id: id,
                position: position,
                name: name,
                deadline: date,
                description: description,
                state: 0
            })
        })
    }

    handleTodoItemClick(action: number, position: number) {
        console.log("action: " + action + ", position: " + position)

        switch (action) {
            case 0:
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
                break
            case 1:
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
                break
            case 2:
                const newItemsList: Array<ITodo> = [] as ITodo[]

                for(let i = 0; i < this.state.items.length; i++)
                    if(this.state.items[i].position != position) {
                        if(this.state.items[i].position > position) this.state.items[i].position -= 1
                        newItemsList.push(this.state.items[i])
                    }

                this.setState({
                    items: newItemsList
                })
                break
        }
    }

    render() {
        console.log(this.state.items)
        return (
            <>
                <Grid item>
                    <NewTodo onClick={
                        (name: string, desc: string) => this.handleNewTodoItemClick(name, desc)
                    }/>
                </Grid>
                <Grid item>
                    <Todos items={this.state.items} onClick={(index: number, position: number) => this.handleTodoItemClick(index, position)}/>
                </Grid>
            </>
        )
    }
}