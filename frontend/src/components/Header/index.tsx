import {AppBar, IconButton, Link, Slide, styled, Toolbar, Typography, useScrollTrigger} from "@mui/material";
import GitHubIcon from '@mui/icons-material/GitHub';
import React from "react";

const Offset = styled('div')(({ theme }) => theme.mixins.toolbar);

interface Props {
    /**
     * Injected by the documentation to work in an iframe.
     * You won't need it on your project.
     */
    window?: () => Window;
    children: React.ReactElement;
}

function HideOnScroll(props: Props) {
    const { children } = props;
    const trigger = useScrollTrigger();

    return (
        <Slide appear={false} direction="down" in={!trigger}>
            {children}
        </Slide>
    );
}

interface MyAppBarProp {
    title: string
}

export function Header(props: MyAppBarProp) {
    return (
        <>
            <HideOnScroll>
                <AppBar position="fixed">
                    <Toolbar>
                        <Link
                            href="https://github.com/gergoradeczki/temalabor-2021"
                            target="_blank"
                            rel="noreferrer">
                            <IconButton>
                                <GitHubIcon/>
                            </IconButton>
                        </Link>
                        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                            {props.title}
                        </Typography>
                    </Toolbar>
                </AppBar>
            </HideOnScroll>
            <Offset/>
        </>
    )
}