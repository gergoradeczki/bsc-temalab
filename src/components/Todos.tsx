import {Paper} from "@mui/material";
import React from "react";
import {Todo} from "./Todo";
import {ITodo} from "../mock";

interface TodosProps {
    items: Array<ITodo>
    onClick: any
}

class Todos extends React.Component<TodosProps, any> {
    constructor(props: any) {
        super(props);
        this.props.items.sort((a, b) => (a.position > b.position) ? 1 : (b.position > a.position) ? -1 : 0)
    }

    render() {
        return (
            <Paper elevation={0}>
                {this.props.items.map((e, index) => (
                    <Todo
                        key={e.id}
                        id={e.id}
                        position={e.position}
                        name={e.name}
                        deadline={e.deadline}
                        description={e.description}
                        state={e.state}
                        onClick={this.props.onClick}
                    />
                ))}
            </Paper>
        )
    }
}

export { Todos }