import {
    Box,
    Button,
    Card,
    CardActions,
    CardContent,
    Paper, TextField
} from "@mui/material";
import React from "react";

interface NewTodoProps {
    onClick: any
}

interface NewTodoStates {
    name?: string
    description?: string
}

class NewTodo extends React.Component<NewTodoProps, NewTodoStates> {
    constructor(props: any) {
        super(props);
        this.state = {
            name: "",
            description: ""
        }
    }



    render() {
        return (
            <Paper elevation={0}>
                <Card sx={{ m: 2, maxWidth: 275 }} >
                    <CardContent>
                        <Box component="form" sx={{'& > :not(style)': { m: 1, width: '25ch' },}}>
                            <TextField
                                id="todo-name"
                                label="Név"
                                variant="outlined"
                                onChange={(e) => this.setState({name: e.target.value})}
                            />
                            <TextField
                                id="todo-description"
                                label="Leírás"
                                variant="outlined"
                                multiline
                                rows={4}
                                onChange={(e) => this.setState({description: e.target.value})}
                            />
                        </Box>
                    </CardContent>
                    <CardActions>
                        <Button onClick={() => this.props.onClick(this.state.name, this.state.description)}>Új TODO felvétele</Button>
                    </CardActions>
                </Card>
            </Paper>
        )
    }
}

export { NewTodo }