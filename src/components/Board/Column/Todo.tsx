import {Button, Card, CardActions, CardContent, IconButton, Typography} from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import React from "react";

enum TodoState {
    PendingState,
    ProgressState,
    DoneState,
    PostponedState
}

interface TodoProps {
    id: number
    position: number
    name: string
    description: string
    deadline: Date
    state: TodoState
    onClick: (index: number, position: number) => void
}

interface TodoStates {

}

class Todo extends React.Component<TodoProps, TodoStates> {
    constructor(props: TodoProps) {
        super(props);
    }

    render() {
        return (
            <Card sx={{ m: 2, maxWidth: 275 }} >
                <CardContent>
                    <Typography variant="h5" component="div">{this.props.name}</Typography>
                    <Typography variant="caption">{this.props.deadline.toLocaleString()}</Typography>
                    <Typography variant="body2" color="text.secondary">{this.props.description}</Typography>
                    <Typography variant="caption">id: {this.props.id}, pos: {this.props.position}, state: {this.props.state.toString()}</Typography>
                </CardContent>
                <CardActions>
                    <IconButton>
                        <EditIcon/>
                    </IconButton>
                    <Button onClick={() => this.props.onClick(0, this.props.position)}>Up</Button>
                    <Button onClick={() => this.props.onClick(1, this.props.position)}>Down</Button>
                    <IconButton onClick={() => this.props.onClick(2, this.props.position)}>
                        <DeleteIcon/>
                    </IconButton>
                </CardActions>
            </Card>
        )
    }
}

export { Todo, TodoState }